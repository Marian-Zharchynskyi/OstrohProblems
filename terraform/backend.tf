# ECR Repository
resource "aws_ecr_repository" "api" {
  name                 = "${var.project_name}-api"
  image_tag_mutability = "MUTABLE"

  image_scanning_configuration {
    scan_on_push = true
  }
}

# Application Load Balancer Security Group
resource "aws_security_group" "alb" {
  name        = "${var.project_name}-alb-sg"
  description = "Security group for ALB"
  vpc_id      = aws_vpc.main.id

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  
  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ECS Tasks Security Group
resource "aws_security_group" "ecs_tasks" {
  name        = "${var.project_name}-ecs-tasks-sg"
  description = "Allow inbound access from the ALB only"
  vpc_id      = aws_vpc.main.id

  ingress {
    from_port       = 8080 # Assuming the .NET API runs on 8080 or 80. Adjust as needed.
    to_port         = 8080
    protocol        = "tcp"
    security_groups = [aws_security_group.alb.id]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ALB
resource "aws_lb" "api_alb" {
  name               = "${var.project_name}-alb"
  internal           = false
  load_balancer_type = "application"
  security_groups    = [aws_security_group.alb.id]
  subnets            = [aws_subnet.public_1.id, aws_subnet.public_2.id]
}

resource "aws_lb_target_group" "api_tg" {
  name        = "${var.project_name}-tg"
  port        = 8080
  protocol    = "HTTP"
  vpc_id      = aws_vpc.main.id
  target_type = "ip"

  health_check {
    healthy_threshold   = "3"
    interval            = "30"
    protocol            = "HTTP"
    matcher             = "200"
    timeout             = "3"
    path                = "/health" # Ensure there is a healthcheck endpoint in the app
    unhealthy_threshold = "2"
  }
}

resource "aws_lb_listener" "api_listener" {
  load_balancer_arn = aws_lb.api_alb.arn
  port              = "80"
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.api_tg.arn
  }
}

# ECS Cluster
resource "aws_ecs_cluster" "main" {
  name = "${var.project_name}-cluster"
}

# IAM Role for ECS Task Execution
resource "aws_iam_role" "ecs_task_execution_role" {
  name = "${var.project_name}-ecs-task-execution-role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Action = "sts:AssumeRole"
        Effect = "Allow"
        Principal = {
          Service = "ecs-tasks.amazonaws.com"
        }
      }
    ]
  })
}

resource "aws_iam_role_policy_attachment" "ecs_task_execution_role_policy" {
  role       = aws_iam_role.ecs_task_execution_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}

resource "aws_iam_role_policy_attachment" "ecs_task_secrets_manager" {
  role       = aws_iam_role.ecs_task_execution_role.name
  policy_arn = "arn:aws:iam::aws:policy/SecretsManagerReadWrite"
}

# ECS Task Definition (Using latest pushed image)
data "aws_ecr_image" "api_image" {
  repository_name = aws_ecr_repository.api.name
  image_tag       = "latest"
  # This might fail on first apply if image is not pushed yet.
  # Often people use a dummy image on first run: "nginxdemos/hello"
  count = 0 
}

resource "aws_ecs_task_definition" "api" {
  family                   = "${var.project_name}-api-task"
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = 256
  memory                   = 512
  execution_role_arn       = aws_iam_role.ecs_task_execution_role.arn

  container_definitions = jsonencode([
    {
      name      = "api"
      image     = "${aws_ecr_repository.api.repository_url}:latest" # Ensure to push the image first or use a dummy for bootstrap
      essential = true
      portMappings = [
        {
          containerPort = 8080
          hostPort      = 8080
        }
      ]
      environment = [
        {
          name  = "ConnectionStrings__DefaultConnection"
          value = "Server=${aws_db_instance.postgres.address};Port=5432;Database=${aws_db_instance.postgres.db_name};User Id=${aws_db_instance.postgres.username};Password=${var.db_password};"
        },
        {
          name = "ASPNETCORE_URLS"
          value = "http://+:8080"
        },
        {
          name  = "AllowedOrigins__0"
          value = "https://${aws_cloudfront_distribution.cdn.domain_name}"
        },
        {
          name  = "AllowedOrigins__1"
          value = "http://localhost:5173"
        }
      ]

      secrets = [
        {
          name      = "Clerk__SecretKey"
          valueFrom = aws_secretsmanager_secret.clerk_secret_key.arn
        },
        {
          name      = "Clerk__Domain"
          valueFrom = aws_secretsmanager_secret.clerk_domain.arn
        },
        {
          name      = "BackblazeB2__KeyId"
          valueFrom = aws_secretsmanager_secret.backblaze_key_id.arn
        },
        {
          name      = "BackblazeB2__ApplicationKey"
          valueFrom = aws_secretsmanager_secret.backblaze_application_key.arn
        },
        {
          name      = "BackblazeB2__BucketName"
          valueFrom = aws_secretsmanager_secret.backblaze_bucket_name.arn
        },
        {
          name      = "BackblazeB2__BucketId"
          valueFrom = aws_secretsmanager_secret.backblaze_bucket_id.arn
        },
        {
          name      = "BackblazeB2__Endpoint"
          valueFrom = aws_secretsmanager_secret.backblaze_endpoint.arn
        },
        {
          name      = "BackblazeB2__Region"
          valueFrom = aws_secretsmanager_secret.backblaze_region.arn
        },
        {
          name      = "AuthSettings__key"
          valueFrom = aws_secretsmanager_secret.jwt_key.arn
        },
        {
          name      = "AuthSettings__issuer"
          valueFrom = aws_secretsmanager_secret.jwt_issuer.arn
        },
        {
          name      = "AuthSettings__audience"
          valueFrom = aws_secretsmanager_secret.jwt_audience.arn
        },
        {
          name      = "Gemini__ApiKey"
          valueFrom = aws_secretsmanager_secret.gemini_api_key.arn
        }
      ]
      logConfiguration = {
        logDriver = "awslogs"
        options = {
          "awslogs-group"         = "/ecs/${var.project_name}-api"
          "awslogs-region"        = var.aws_region
          "awslogs-stream-prefix" = "ecs"
        }
      }
    }
  ])
}

# CloudWatch Log Group for ECS
resource "aws_cloudwatch_log_group" "ecs_logs" {
  name              = "/ecs/${var.project_name}-api"
  retention_in_days = 7
}

# ECS Service
resource "aws_ecs_service" "api" {
  name            = "${var.project_name}-service"
  cluster         = aws_ecs_cluster.main.id
  task_definition = aws_ecs_task_definition.api.arn
  desired_count   = 0
  launch_type     = "FARGATE"

  lifecycle {
    ignore_changes = [desired_count]
  }

  network_configuration {
    security_groups  = [aws_security_group.ecs_tasks.id]
    subnets          = [aws_subnet.public_1.id, aws_subnet.public_2.id]
    assign_public_ip = true # Required for pulling images without a NAT gateway
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.api_tg.arn
    container_name   = "api"
    container_port   = 8080
  }

  depends_on = [aws_lb_listener.api_listener]
}
