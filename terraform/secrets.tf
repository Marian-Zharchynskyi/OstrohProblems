# AWS Secrets Manager for storing API keys securely

resource "aws_secretsmanager_secret" "clerk_secret_key" {
  name = "${var.project_name}/clerk-secret-key"
}

resource "aws_secretsmanager_secret_version" "clerk_secret_key" {
  secret_id     = aws_secretsmanager_secret.clerk_secret_key.id
  secret_string = var.clerk_secret_key
}

resource "aws_secretsmanager_secret" "clerk_domain" {
  name = "${var.project_name}/clerk-domain"
}

resource "aws_secretsmanager_secret_version" "clerk_domain" {
  secret_id     = aws_secretsmanager_secret.clerk_domain.id
  secret_string = var.clerk_domain
}

resource "aws_secretsmanager_secret" "backblaze_key_id" {
  name = "${var.project_name}/backblaze-key-id"
}

resource "aws_secretsmanager_secret_version" "backblaze_key_id" {
  secret_id     = aws_secretsmanager_secret.backblaze_key_id.id
  secret_string = var.backblaze_key_id
}

resource "aws_secretsmanager_secret" "backblaze_application_key" {
  name = "${var.project_name}/backblaze-application-key"
}

resource "aws_secretsmanager_secret_version" "backblaze_application_key" {
  secret_id     = aws_secretsmanager_secret.backblaze_application_key.id
  secret_string = var.backblaze_application_key
}

resource "aws_secretsmanager_secret" "backblaze_bucket_name" {
  name = "${var.project_name}/backblaze-bucket-name"
}

resource "aws_secretsmanager_secret_version" "backblaze_bucket_name" {
  secret_id     = aws_secretsmanager_secret.backblaze_bucket_name.id
  secret_string = var.backblaze_bucket_name
}

resource "aws_secretsmanager_secret" "backblaze_bucket_id" {
  name = "${var.project_name}/backblaze-bucket-id"
}

resource "aws_secretsmanager_secret_version" "backblaze_bucket_id" {
  secret_id     = aws_secretsmanager_secret.backblaze_bucket_id.id
  secret_string = var.backblaze_bucket_id
}

resource "aws_secretsmanager_secret" "backblaze_endpoint" {
  name = "${var.project_name}/backblaze-endpoint"
}

resource "aws_secretsmanager_secret_version" "backblaze_endpoint" {
  secret_id     = aws_secretsmanager_secret.backblaze_endpoint.id
  secret_string = var.backblaze_endpoint
}

resource "aws_secretsmanager_secret" "backblaze_region" {
  name = "${var.project_name}/backblaze-region"
}

resource "aws_secretsmanager_secret_version" "backblaze_region" {
  secret_id     = aws_secretsmanager_secret.backblaze_region.id
  secret_string = var.backblaze_region
}

resource "aws_secretsmanager_secret" "gemini_api_key" {
  name = "${var.project_name}/gemini-api-key"
}

resource "aws_secretsmanager_secret_version" "gemini_api_key" {
  secret_id     = aws_secretsmanager_secret.gemini_api_key.id
  secret_string = var.gemini_api_key
}

resource "aws_secretsmanager_secret" "jwt_key" {
  name = "${var.project_name}/jwt-key"
}

resource "aws_secretsmanager_secret_version" "jwt_key" {
  secret_id     = aws_secretsmanager_secret.jwt_key.id
  secret_string = var.jwt_key
}

resource "aws_secretsmanager_secret" "jwt_issuer" {
  name = "${var.project_name}/jwt-issuer"
}

resource "aws_secretsmanager_secret_version" "jwt_issuer" {
  secret_id     = aws_secretsmanager_secret.jwt_issuer.id
  secret_string = var.jwt_issuer
}

resource "aws_secretsmanager_secret" "jwt_audience" {
  name = "${var.project_name}/jwt-audience"
}

resource "aws_secretsmanager_secret_version" "jwt_audience" {
  secret_id     = aws_secretsmanager_secret.jwt_audience.id
  secret_string = var.jwt_audience
}
