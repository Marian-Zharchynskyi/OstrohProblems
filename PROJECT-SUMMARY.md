# Ostroh Problems - Project Summary

## 📋 Project Overview
Ostroh Problems is a comprehensive web application for reporting, tracking, and resolving urban issues in the city of Ostroh. The system connects citizens, coordinators, and administrators to facilitate efficient problem resolution.

## ✅ Completed Implementation

### 🏗️ Architecture

#### Backend (API)
- **Clean Architecture** with 4 layers:
  - **API**: Controllers, DTOs, Filters, Middleware.
  - **Application**: Business logic, CQRS with MediatR, Validators (FluentValidation), Interfaces.
  - **Domain**: Entities (`Problem`, `Category`, `User`, etc.), Enums, Value Objects.
  - **Infrastructure**: Persistence (EF Core), Services implementation (`ImageService`, `SignalRService`, `IdentityService`).
- **Database**: PostgreSQL with Entity Framework Core.
- **Real-time**: SignalR for live updates (Comments, Notifications).
- **Storage**: Local file system storage for images.

#### Frontend (Client)
- **Feature-based Architecture**: Modular structure (`features/` containing api, hooks, components).
- **Tech Stack**: React 19, TypeScript, Vite.
- **State Management**: React Query (TanStack Query) for server state.
- **UI Framework**: TailwindCSS + ShadCN UI.
- **Routing**: React Router DOM with role-based protection.

### 📦 Technology Stack

| Area | Technologies |
|------|--------------|
| **Backend** | .NET 8, ASP.NET Core, EF Core, MediatR, FluentValidation, SignalR, Npgsql |
| **Frontend** | React 19, TypeScript, Vite, Axios, React Query, React Hook Form, Zod |
| **Database** | PostgreSQL |
| **Styling** | TailwindCSS, ShadCN UI, Lucide React |
| **Tools** | Docker (ready), Swagger UI |

### 🎨 Features Implemented

#### 1. Problems Management
- **Reporting**: Citizens can create problems with Title, Description, Location (Lat/Lon), Categories, and Photos.
- **Workflow**: Status lifecycle (`New` -> `InProgress` -> `Completed` or `Rejected`).
- **Coordination**:
  - Assign coordinators to problems.
  - Coordinator dashboard to manage assigned tasks.
  - Rejection with reason and Restore functionality.
  - Update current state/progress.
- **Discovery**: List views, filtering by status/user/coordinator, Map view.

#### 2. Categories Management
- **Full CRUD**: Administrators can Create, Read, Update, and Delete problem categories.
- **Integration**: Categories are linked to problems (Many-to-Many).

#### 3. User & Role Management
- **Authentication**: JWT-based auth (Login, Register).
- **Roles**:
  - `Administrator`: Full access, manage users, categories, and all problems.
  - `Coordinator`: Manage assigned problems, update status.
  - `User`: Report problems, track own submissions.
- **Profile**: Update personal info and upload profile picture.
- **Admin Panel**: User management table (list, create, edit, delete users).

#### 4. Interaction
- **Comments**: Real-time commenting system on problems.
- **Ratings**: Users can rate the resolution quality.
- **Notifications**: SignalR-based notifications (infrastructure ready).

### � Technical Details

#### API Structure
- **Problems**: `/problems` (CRUD, Status updates, Image upload, Assignment)
- **Categories**: `/problem-categories` (CRUD)
- **Auth**: `/account` (Login, Register, Refresh Token)
- **Users**: `/users` (CRUD, Image upload)
- **Comments**: `/comments` (CRUD, Real-time)
- **Ratings**: `/ratings` (CRUD)

#### Image Handling
- **Service**: `ImageService` handles file operations.
- **Storage**: Images are saved locally in the `uploads` directory.
- **Entities**: `ProblemImage` and `UserImage` store file paths.

#### Real-time (SignalR)
- **Hubs**: `CommentsHub`, `NotificationsHub`.
- **Flow**: Clients join groups based on Problem ID to receive live comment updates.

## 🚀 Getting Started

### Prerequisites
- Node.js & npm
- .NET SDK
- PostgreSQL

### Setup
1. **Database**: Update connection string in `appsettings.json` and run migrations (`dotnet ef database update`).
2. **Backend**: Run the API project (`dotnet run`).
3. **Frontend**:
   ```bash
   cd client
   npm install
   npm run dev
   ```

## 🔮 Future Enhancements
- [ ] Advanced Map integration (interactive picking).
- [ ] Email notifications.
- [ ] Public stats dashboard.
- [ ] Mobile-responsive optimizations for field reporting.
- [ ] Unit and Integration tests coverage.

## ✨ Summary
The project has evolved into a robust full-stack solution. The backend now supports a complex workflow for problem resolution with role-based security. The frontend provides a modern, responsive interface for all user roles, leveraging real-time capabilities and efficient state management. The transition to local image storage and full entity-based Categories is complete.
