# Ostroh Problems - City Issue Tracker

[Українська версія нижче](#ostroh-problems---міський-трекер-проблем)

---

## 📋 About The Project
**Ostroh Problems** is a comprehensive web platform for reporting, tracking, and resolving civic issues in the city of Ostroh. The system creates a unified space for interaction between citizens, service coordinators, and the city administration for the effective improvement of the urban environment.

The main goal is to ensure transparency in resolving communal and infrastructural issues, providing a convenient tool for reporting problems and getting feedback.

---

## 👥 Roles & Capabilities
The system is built on a Role-Based Access Control (RBAC) model, where each user has clearly defined permissions.

### 1. User
A regular resident of the city or a registered guest.
* **Create issues**: Report new issues by adding a title, description, category, location (coordinates), and photos.
* **View**: Access lists of their own reported issues and a general list of public issues.
* **Edit**: Modify the description, title, and categories of issues they created (up to a certain workflow stage).
* **Interact**: Leave comments under issues and participate in discussions.
* **Rate**: Evaluate the quality of the issue resolution after it is closed.
* **Profile**: Manage their own profile (change avatar, personal info).

### 2. Coordinator
An authorized person (representative of utility services or the city council) responsible for resolving issues.
* **Workflow Management**:
  - Accept issues into work (`StartProblem`).
  - Update current execution progress (`UpdateCurrentState`).
  - Complete issues (`CompleteProblem`).
  - Reject issues with a specified reason (`Reject`).
  - Restore rejected issues (`Restore`).
* **Reporting**: Upload photo reports of completed work (`UploadCoordinatorImages`).
* **Communication**: Leave official coordinator comments (`SetCoordinatorComment`), which are visually highlighted.
* **Data Editing**: Correct issue locations (coordinates) if the user specified them inaccurately.
* **Access**: View all issues but focus on those assigned to them or matching their profile.

### 3. Administrator
Has full control over the system.
* **User Management**: View all users, create, edit, and delete accounts.
* **Category Management**: Create and edit the directory of issue categories (e.g., "Roads", "Lighting", "Waste").
* **Moderation**: Delete inappropriate issues, comments, or photos.
* **Assignment**: Assign coordinators to specific issues (`AssignCoordinator`).
* **All Roles' Permissions**: Has access to all functions of both users and coordinators.

---

## ⚙️ Issue Lifecycle (Workflow)
Issues pass through several states (Status) representing their resolution progress:
1. **New**: The issue has been created by a user and is awaiting review.
2. **InProgress**: A coordinator has accepted the issue and started working on it.
3. **Completed**: The work is finished, photo reports are added (optional), and the issue is resolved.
4. **Rejected**: The issue is rejected by a coordinator or admin (e.g., duplicate, irrelevant, out of jurisdiction) with a mandatory reason.

> **Additional features**:
> * A rejected issue can be **Restored** to return it to an active state.
> * Users can leave **Ratings** (1-5 stars) and **Comments** at any stage (depending on settings).

---

## 🏗️ Architecture & Technologies
The project is implemented as a **Full-Stack Web Application** using modern patterns.

### Backend (Server)
* **Platform**: .NET 10, ASP.NET Core Web API, C# 13.
* **Architecture**: Clean Architecture (Domain, Application, Infrastructure, API layers).
* **Database**: PostgreSQL + Entity Framework Core (Code First).
* **Real-time**: SignalR (for live comments and notifications).
* **Validation**: FluentValidation.
* **CQRS**: MediatR pattern for segregating commands and queries.
* **Files**: Local or S3 storage integration.

### Frontend (Client)
* **Framework**: React 19 + TypeScript (powered by Vite).
* **UI/UX**: TailwindCSS + ShadCN UI (Radix Primitives).
* **State Management**: TanStack Query (React Query v5) for server state.
* **Forms**: React Hook Form + Zod.
* **Maps**: Leaflet & React-Leaflet for interactive geolocation picker.

---

## 🚀 Functional Modules

### 1. Issues Core
* CRUD operations for city issues.
* Filtering: by status, category, priority, date.
* Search: full-text search across titles/descriptions.
* Sorting & Pagination.

### 2. Interaction Module
* **Comments**: Real-time nested/linear discussion under each issue.
* **Ratings**: Feedback evaluation system for coordinator performance.

### 3. Gallery
* Multi-file image uploading for users.
* Coordinator report sections ("Before/After" photos).
* Image optimization and secure storage.

### 4. Admin Dashboard
* Reference directories management (Categories).
* User management table with filters and editing capabilities.

---

## 📦 Database Schema
The system uses **PostgreSQL** relational database managed via **Entity Framework Core** Code First.

### Key Entities:
1. **Users (`users`)**: Local sync copy of Clerk users containing `id`, `clerk_id`, `email`, and `role_id`.
2. **Roles (`roles`)**: Defines permissions (User, Coordinator, Administrator).
3. **Problems (`problems`)**: Central entity containing `title`, `description`, `status`, `priority`, location coordinates, and creation details.
4. **Comments (`comments`)**: Textual discussion messages mapped to problems.
5. **Ratings (`ratings`)**: Post-resolution feedback ratings (1-5 stars).

---

## 🔌 API Architecture (Clean Architecture)
The API strictly separates concerns using **CQRS** (Command Query Responsibility Segregation) via MediatR:
1. **Domain Layer**: Core entities, value objects, and repository interfaces. No external dependencies.
2. **Application Layer**: Business logic, MediatR handlers, validation rules (FluentValidation), and port interfaces.
3. **Infrastructure Layer**: Concrete implementations of adapters (PostgreSQL DbContext, SignalR Hubs, Clerk user sync, Backblaze B2 storage).
4. **API Layer**: Presentation controllers, middlewares (global exception handling, JWT authentication, Clerk sync), and DI container setup.

---

## 🤖 Google Gemini AI Integration
The platform leverages generative AI capabilities (LLM) via Google Gemini API to streamline user experience:
1. **Voice Assistant Reporting**: Users record audio in the browser (WebM), which is processed by Gemini API to perform speech-to-text transcription and Named Entity Recognition (NER). Structured JSON is generated containing location details, description, title, and category.
2. **AI Chatbot Consultant**: An interactive chatbot helper powered by Zero-Shot Classification prompts to guide users through platform rules and navigation.
3. **Safety & Formatting**: Automatically filters profanity and structures AI responses for readability.

---

## ☁️ File Storage (Backblaze B2)
* Scalable image storage utilizing AWS S3 compatibility APIs.
* Managed through `BackblazeB2StorageService` implementing `IStorageService` interface.
* Guarantees unique file name generation (GUID) and strict MIME-type validation.

---

## 🔐 Identity & Authentication (Clerk)
* Authentication, OAuth (Google), and session tokens are managed via **Clerk React SDK**.
* Clerk webhooks (`user.created`, `user.updated`, `user.deleted`) synchronize profiles with the local PostgreSQL database dynamically.
* Custom user metadata keeps role assignments synchronized.

---

## ☁️ Deployment & Infrastructure (AWS & Terraform)
* **Terraform**: The infrastructure is declared as code (`terraform/`) containing configurations for VPC, public/private subnets, Security Groups, Load Balancers, and routing.
* **AWS ECS Fargate**: Zero-server container execution for API containers with auto-scaling capabilities.
* **Application Load Balancer (ALB)**: Manages incoming traffic routing and health checks.
* **Amazon RDS PostgreSQL**: Database instance isolated in a private subnet.
* **CI/CD (GitHub Actions)**: Automatic Docker image building and deployment to ECR/ECS Fargate on every push to the `main` branch. Upgraded to use **.NET 10.0 SDK**.

---

## 🛠️ Setup & Launch

### Prerequisites
* Node.js (v18+)
* .NET 10 SDK
* PostgreSQL Database
* Docker (optional)

### Setup Steps
1. **Database**: Configure your ConnectionString in `appsettings.json` (under `DefaultConnection`).
2. **Migrations**: Apply DB migrations by running `dotnet ef database update` inside the API project.
3. **Start API**: Run `dotnet run` inside the API presentation project.
4. **Start Frontend**:
   ```bash
   cd client
   npm install
   npm run dev
   ```
5. Open `http://localhost:3000` or `http://localhost:5173` in your browser.

---
---

# Ostroh Problems - Міський Трекер Проблем

## 📋 Про Проект
**Ostroh Problems** — це комплексна веб-платформа для повідомлення, відстеження та вирішення міських проблем у місті Острог. Система створює єдиний простір для взаємодії між мешканцями, координаторами служб та адміністрацією міста задля ефективного покращення міського середовища.

Головна мета — забезпечити прозорість у вирішенні комунальних та інфраструктурних питань, надати зручний інструмент фіксації проблем та зворотного зв'язку.

---

## 👥 Ролі та Їх Можливості
Система побудована на рольовій моделі доступу (RBAC), де кожен користувач має чітко визначені повноваження.

### 1. Користувач (User)
Звичайний мешканець міста або зареєстрований гість.
* **Створення проблем**: Може повідомляти про нові проблеми, додаючи заголовок, опис, категорію, локацію (координати) та фотографії.
* **Перегляд**: Має доступ до списку власних проблем та загального списку публічних проблем.
* **Редагування**: Може змінювати опис, заголовок та категорії створених ним проблем (до певного етапу).
* **Взаємодія**: Може залишати коментарі до проблем, брати участь у обговореннях.
* **Оцінка**: Може оцінювати якість вирішення проблеми після її закриття.
* **Профіль**: Управління власним профілем (зміна фото, даних).

### 2. Координатор (Coordinator)
Відповідальна особа (представник комунальних служб або міської ради), що займається безпосереднім вирішенням проблем.
* **Управління процесом**:
  - Приймати проблеми в роботу (`StartProblem`).
  - Змінювати поточний стан виконання (`UpdateCurrentState`).
  - Завершувати виконання проблеми (`CompleteProblem`).
  - Відхиляти проблеми із зазначенням причини (`Reject`).
  - Відновлювати відхилені проблеми (`Restore`).
* **Звітування**: Завантажувати фото-звіти про виконану роботу (`UploadCoordinatorImages`).
* **Комунікація**: Залишати офіційні коментарі координатора (`SetCoordinatorComment`), які виділяються візуально.
* **Редагування даних**: Має право уточнювати локацію проблеми (координати), якщо користувач вказав її неточно.
* **Доступ**: Бачить всі проблеми, але фокусується на призначених йому або тих, що відповідають його профілю.

### 3. Адміністратор (Administrator)
Має повний контроль над системою.
* **Управління користувачами**: Перегляд списку всіх користувачів, створення, редагування та видалення облікових записів.
* **Управління категоріями**: Створення та редагування довідника категорій проблем (напр., "Дороги", "Освітлення", "Сміття").
* **Модерація**: Може видаляти некоректні проблеми, коментарі або фотографії.
* **Призначення**: Може призначати координаторів на конкретні проблеми (`AssignCoordinator`).
* **Усі права інших ролей**: Має доступ до функціоналу користувача та координатора.

---

## ⚙️ Життєвий Цикл Проблеми (Workflow)
Проблема проходить через кілька станів (Status), які відображають етап її вирішення:
1. **Нова (New)**: Проблема створена користувачем і очікує на розгляд.
2. **В роботі (InProgress)**: Координатор прийняв проблему та почав над нею працювати.
3. **Виконано (Completed)**: Роботи завершені, додано фото-звіт (опціонально), проблема вирішена.
4. **Відхилено (Rejected)**: Проблема відхилена координатором або адміністратором з обов'язковою вказівкою причини.

> **Додатково**:
> * Відхилену проблему можна **Відновити (Restore)**, повертаючи її до активного стану.
> * Користувачі можуть ставити **Оцінки (Ratings)** та лишати **Коментарі** на будь-якому етапі.

---

## 🏗️ Архітектура та Технології
Проект реалізовано як **Full-Stack Web Application** з використанням сучасних підходів.

### Backend (Server)
* **Платформа**: .NET 10, ASP.NET Core Web API, C# 13.
* **Архітектура**: Clean Architecture (Domain, Application, Infrastructure, API layers).
* **База даних**: PostgreSQL + Entity Framework Core (Code First).
* **Real-time**: SignalR (для живих коментарів та повідомлень).
* **Валідація**: FluentValidation.
* **CQRS**: MediatR pattern для розділення команд та запитів.
* **Файли**: Локальне або хмарне збереження зображень.

### Frontend (Client)
* **Фреймворк**: React 19 + TypeScript (на базі Vite).
* **UI/UX**: TailwindCSS + ShadCN UI (Radix Primitives).
* **State Management**: React Query (TanStack Query v5) для серверного стану.
* **Форми**: React Hook Form + Zod.
* **Карти**: Leaflet & React-Leaflet для вибору геолокації.

---

## 🚀 Функціональні Модулі

### 1. Модуль Проблем (Problems Core)
* CRUD операції для проблем.
* Фільтрація: за статусом, категорією, пріоритетом, датою.
* Пошук: текстовий пошук по заголовку/опису.
* Сортування та пагінація списків.

### 2. Модуль Взаємодії
* **Коментарі**: Деревоподібні або лінійні обговорення в реальному часі.
* **Рейтинги**: Система оцінювання якості роботи служб.

### 3. Галерея
* Підтримка мульти-завантаження фото користувачами.
* Фото-звіти координаторів ("Було/Стало").
* Оптимізація та безпечне зберігання файлів.

### 4. Адмін-панель
* Керування довідниками (Категорії).
* Таблиця користувачів з фільтрами та можливостями редагування.

---

## 📦 База Даних (Database)
Система використовує реляційну базу даних **PostgreSQL**, взаємодія з якою відбувається через **Entity Framework Core** Code First.

### Основні Сутності (Entities):
1. **Users (`users`)**: Синхронізована копія користувачів з Clerk (`id`, `clerk_id`, `email`, `role_id`).
2. **Roles (`roles`)**: Визначає права доступу (User, Coordinator, Administrator).
3. **Problems (`problems`)**: Головна сутність (`title`, `description`, `status`, `priority`, `latitude`, `longitude`, `created_by`).
4. **Comments (`comments`)**: Текстові повідомлення до проблем.
5. **Ratings (`ratings`)**: Оцінки якості вирішення проблеми (1-5 зірок).

---

## 🔌 API Архітектура (Clean Architecture)
API побудовано за принципами Clean Architecture та CQRS (MediatR):
1. **Domain Layer**: Сутності, Enums та інтерфейси репозиторіїв. Не має зовнішніх залежностей.
2. **Application Layer**: Бізнес-логіка, MediatR Commands/Queries, валідація (FluentValidation) та інтерфейси сервісів.
3. **Infrastructure Layer**: Доступ до БД (PostgreSQL DbContext), SignalR Hubs, Clerk, Backblaze B2.
4. **API Layer**: REST Controllers, DI контейнер (`Program.cs`), Middlewares.

---

## 🤖 Інтеграція Штучного Інтелекту (Google Gemini AI)
Система використовує можливості великих мовних моделей (LLM) через Gemini API:
1. **Голосовий асистент**: Користувачі записують голос (WebM), який Gemini транскрибує та видобуває дані (NER) у Structured JSON для автозаповнення полів проблеми.
2. **Чат-бот консультант**: Інтерактивний помічник на основі Zero-Shot Classification для допомоги користувачам.
3. **Модерація**: Автоматичний фільтр ненормативної лексики (Profanity Filter) та структурування тексту відповідей.

---

## ☁️ Хмарне Сховище (Backblaze B2)
* Надійне збереження файлів через S3-сумісний API за допомогою `BackblazeB2StorageService`.
* Унікальне іменування файлів (GUID) та валідація дозволених MIME-типів.

---

## 🔐 Автентифікація та Ідентифікація (Clerk)
* Управління сесіями за допомогою **Clerk React SDK**.
* Синхронізація локальної БД з Clerk через Webhooks (`user.created`, `user.updated`, `user.deleted`).
* Синхронізація рольової моделі через публічні метадані Clerk.

---

## ☁️ Інфраструктура та Хмарне Розгортання (AWS & Terraform)
* **Terraform**: Інфраструктура описана декларативно (`terraform/`) — VPC, підмережі, Security Groups, ALB.
* **AWS ECS Fargate**: Безсерверний запуск Docker-контейнерів.
* **Application Load Balancer (ALB)**: Маршрутизація вхідного трафіку.
* **Amazon RDS PostgreSQL**: БД ізольована у приватній підмережі.
* **CI/CD (GitHub Actions)**: Пайплайни (`github-pipelines.yml`) автоматично збирають Docker-образи та деплоять їх в ECR/ECS. Оновлено до підтримки **.NET 10.0 SDK**.

---

## 🛠️ Налаштування та Запуск

### Передумови
* Node.js (v18+)
* .NET 10 SDK
* PostgreSQL Database
* Docker (опціонально)

### Інструкція
1. **База даних**: Налаштуйте ConnectionString у `appsettings.json` (секція `DefaultConnection`).
2. **Міграції**: Запустіть `dotnet ef database update` у папці API для створення схеми БД.
3. **Сервер**: Запустіть API командою `dotnet run`.
4. **Клієнт**:
   ```bash
   cd client
   npm install
   npm run dev
   ```
5. Відкрийте браузер за адресою `http://localhost:3000` або `http://localhost:5173`.
