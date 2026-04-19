terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = var.aws_region

  default_tags {
    tags = {
      Project     = var.project_name
      Environment = var.environment
      ManagedBy   = "Terraform"
    }
  }
}

variable "aws_region" {
  type    = string
  default = "eu-central-1"
}

variable "project_name" {
  type    = string
  default = "ostroh-problems"
}

variable "environment" {
  type    = string
  default = "dev"
}

resource "aws_resourcegroups_group" "resource_group" {
  name = "${var.project_name}-${var.environment}-rg"

  resource_query {
    query = jsonencode({
      ResourceTypeFilters = ["AWS::AllSupported"]
      TagFilters = [
        {
          Key    = "Project"
          Values = [var.project_name]
        },
        {
          Key    = "Environment"
          Values = [var.environment]
        }
      ]
    })
  }
}

variable "db_password" {
  type      = string
  sensitive = true
}

variable "clerk_secret_key" {
  type      = string
  sensitive = true
}

variable "clerk_domain" {
  type      = string
}

variable "backblaze_key_id" {
  type      = string
  sensitive = true
}

variable "backblaze_application_key" {
  type      = string
  sensitive = true
}

variable "backblaze_bucket_name" {
  type      = string
}

variable "backblaze_bucket_id" {
  type      = string
}

variable "backblaze_endpoint" {
  type      = string
  default   = "s3.eu-central-003.backblazeb2.com"
}

variable "backblaze_region" {
  type      = string
  default   = "eu-central-003"
}

variable "gemini_api_key" {
  type      = string
  sensitive = true
}

variable "jwt_key" {
  type      = string
  sensitive = true
}

variable "jwt_issuer" {
  type      = string
  default   = "ostroh-problems"
}

variable "jwt_audience" {
  type      = string
  default   = "ostroh-problems-api"
}

output "api_url" {
  value = aws_lb.api_alb.dns_name
}

output "frontend_url" {
  value = aws_cloudfront_distribution.cdn.domain_name
}

output "cloudfront_id" {
  value = aws_cloudfront_distribution.cdn.id
}

output "ecr_repository_url" {
  value = aws_ecr_repository.api.repository_url
}
