# AWS Deployment Analysis - OstrohProblems

## Запит користувача

Корисувач попросив проаналізувати проект OstrohProblems для визначення необхідних AWS ресурсів для деплою через Terraform, з урахуванням того, що blob storage вже розміщено на Backblaze B2.

## Що було проаналізовано

### Структура проекту

**Backend (.NET 8 Web API)**
- PostgreSQL база даних
- SignalR для real-time (CommentsHub, NotificationsHub)
- Clerk для аутентифікації
- Backblaze B2 для зберігання файлів (S3-compatible)
- Gemini AI service
- JWT токени
- Clean Architecture (Domain, Application, Infrastructure, API layers)

**Frontend (React 19 + Vite)**
- Статичний SPA
- SignalR клієнт
- Clerk для аутентифікації
- Leaflet для карт
- TailwindCSS + ShadCN UI

**Landing (Next.js 16)** - ВИДАЛЕНО з плану (obsolete)

### Конфігурації

**Backend Environment Variables:**
- `ConnectionStrings__DefaultConnection` - PostgreSQL connection string
- `Clerk__Domain` - Clerk домен
- `Clerk__SecretKey` - Clerk секретний ключ
- `BackblazeB2__KeyId` - Backblaze key ID
- `BackblazeB2__ApplicationKey` - Backblaze application key
- `BackblazeB2__BucketName` - Backblaze bucket name
- `BackblazeB2__BucketId` - Backblaze bucket ID
- `BackblazeB2__Endpoint` - Backblaze endpoint (s3.eu-central-003.backblazeb2.com)
- `BackblazeB2__Region` - Backblaze region (eu-central-003)
- `AuthSettings__key` - JWT секретний ключ
- `AuthSettings__issuer` - JWT issuer
- `AuthSettings__audience` - JWT audience
- `Gemini__ApiKey` - Gemini API key

**Frontend Environment Variables:**
- `VITE_API_URL` - URL backend API
- `VITE_CLERK_PUBLISHABLE_KEY` - Clerk publishable key

## Необхідні AWS ресурси

### 1. Для Backend (.NET API)

**App Runner або ECS/Fargate**
- Docker контейнер для .NET API
- Environment variables для конфігурації
- Auto-scaling
- Health checks

**AWS RDS PostgreSQL**
- Managed PostgreSQL база даних
- Multi-AZ для high availability (опціонально)
- Automated backups

**ElastiCache Redis**
- Для SignalR sticky sessions (WebSocket з'єднання)
- Кластерний режим для production
- Automatic failover

**VPC та Networking**
- Private subnets для RDS та Redis
- Public subnets для App Runner/ECS
- Security Groups для обмеження доступу
- NAT Gateway для outbound traffic

**IAM Roles**
- Для доступу до RDS, Redis, CloudWatch
- Principle of least privilege

**CloudWatch**
- Логи та моніторинг
- Alarms для alerting
- Metrics для performance

### 2. Для Frontend (React)

**AWS S3 + CloudFront**
- S3 bucket для статичних файлів
- CloudFront CDN для глобального розповсюдження
- SSL через ACM
- Cache policies для оптимізації

### 3. Спільне

**ACM (Certificate Manager)**
- SSL сертифікати для доменів
- Automatic renewal

**Route 53**
- DNS управління
- Health checks
- Alias records для CloudFront та App Runner

**AWS Secrets Manager**
- Зберігання всіх секретних ключів
- Automatic rotation (опціонально)

## Рекомендації для Terraform

Створити наступні модулі в папці `terraform/`:

```
terraform/
├── modules/
│   ├── vpc/              # VPC, subnets, security groups, NAT Gateway
│   ├── rds/              # PostgreSQL база даних
│   ├── elasticache/      # Redis для SignalR
│   ├── backend/          # App Runner/ECS для .NET API
│   ├── frontend/         # S3 + CloudFront для React
│   ├── dns/              # Route 53 records
│   ├── certificates/     # ACM SSL сертифікати
│   └── secrets/         # AWS Secrets Manager
├── environments/
│   ├── dev/              # Development конфігурація
│   └── prod/             # Production конфігурація
└── main.tf               # Головний файл
```

## Важливі нюанси

1. **SignalR**: Потрібні sticky sessions для WebSocket з'єднань (через Redis або App Runner session affinity)

2. **CORS**: Оновити в production з localhost на реальні домени (frontend CloudFront domain)

3. **Backblaze**: Не створювати S3 на AWS, використовувати існуючий Backblaze B2

4. **Security**: Всі секрети зберігати в AWS Secrets Manager

5. **Database**: Використовувати parameter groups для оптимізації PostgreSQL

6. **Frontend**: React білдити локально або в CI/CD перед завантаженням на S3

7. **CI/CD**: GitHub Actions або AWS CodePipeline для автоматизації деплою

## Відповіді на додаткові питання

### Чи нормально створити Terraform в тому ж репозиторії?

**Відповідь:** Так, це нормальний підхід для малих/середніх проектів.

**Плюси:**
- Простіше підтримувати версійну сумісність між кодом та інфраструктурою
- Зручніше для команди розробників - все в одному місці
- Легше налаштувати CI/CD
- Швидше розуміння зв'язків між кодом та ресурсами

**Мінуси:**
- Відділення відповідальності може бути менш чітким
- Більший репозиторій
- Складніше масштабувати для дуже складної інфраструктури

### Чи є в AWS аналог Azure Resource Group?

**Відповідь:** Прямого аналогу немає, але є альтернативи:

1. **CloudFormation Stack** - найближчий еквівалент
2. **Terraform State** - групує ресурси, якими керує Terraform
3. **AWS CDK App** - групує стеки в CDK
4. **Tags** - теги для групування ресурсів
5. **AWS Organizations OU** - для групування акаунтів

**Для цього проекту:** Кожен Terraform state/stack буде еквівалентом Resource Group.

## Наступні кроки

1. Створити папку `terraform/` в корені проекту
2. Створити модулі для кожного компонента інфраструктури
3. Налаштувати GitHub Actions або AWS CodePipeline для CI/CD
4. Створити Dockerfile для .NET API (якщо ще немає)
5. Налаштувати environment variables в production
6. Оновити CORS конфігурацію в Program.cs

## Дати аналізу

- Початковий аналіз: 13 квітня 2026
- Оновлення (без landing): 14 квітня 2026
- Додаткові питання: 19 квітня 2026
