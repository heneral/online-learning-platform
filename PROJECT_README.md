# 🎓 Online Learning Platform

A comprehensive full-stack online learning management system with **ASP.NET Core 7 Web API** (Backend) and **Angular 17** (Frontend).

## ✨ Features

### 👨‍🎓 For Students
- Browse and search courses by category and level
- Enroll in courses (free or paid)
- Watch video lessons with progress tracking
- Take quizzes and get instant results
- Track learning progress with completion percentages
- View course certificates upon completion

### 👨‍🏫 For Instructors
- Create and manage courses
- Upload video lessons and course materials
- Create quizzes with multiple choice questions
- View student enrollment statistics
- Track course performance and revenue
- Publish/unpublish courses

### 👨‍💼 For Administrators
- Dashboard with platform statistics
- Monitor all users, courses, and enrollments
- View revenue analytics
- Manage platform content

## 🛠️ Tech Stack

### Backend
- **Framework**: ASP.NET Core 7.0 Web API
- **Database**: Entity Framework Core 7 (SQLite/SQL Server/PostgreSQL)
- **Authentication**: ASP.NET Identity + JWT Bearer Tokens
- **API Documentation**: Swagger/OpenAPI
- **Storage**: Azure Blob Storage (for videos)

### Frontend
- **Framework**: Angular 17
- **HTTP Client**: Angular HttpClient with RxJS
- **Routing**: Angular Router with Guards
- **Forms**: Reactive Forms
- **Styling**: Custom CSS

## 📋 Prerequisites

**Backend:**
- .NET 7.0 SDK or later
- SQLite (default) / SQL Server / PostgreSQL

**Frontend:**
- Node.js 18+ and npm
- Angular CLI 17+

## 🚀 Quick Start

### Option 1: Use the Start Script (Easiest)

```bash
# Make script executable
chmod +x start.sh

# Start both backend and frontend
./start.sh
```

This will start:
- Backend API on `http://localhost:5000`
- Frontend app on `http://localhost:4200`

### Option 2: Manual Setup

#### 1. Start Backend

```bash
# From project root
snap run dotnet-sdk.dotnet run

# Or using standard dotnet if installed
dotnet run
```

Backend will be available at: `http://localhost:5000`
API Documentation (Swagger): `http://localhost:5000/swagger`

#### 2. Start Frontend

```bash
# Navigate to frontend directory
cd frontend

# Install dependencies (first time only)
npm install

# Start development server
npm start
```

Frontend will be available at: `http://localhost:4200`

## 🌐 Application URLs

| Service | URL |
|---------|-----|
| Frontend Application | http://localhost:4200 |
| Backend API | http://localhost:5000 |
| Swagger API Docs | http://localhost:5000/swagger |

## 📁 Project Structure

```
online-learning-platform/
├── Controllers/              # API Controllers
│   ├── AuthController.cs         # Authentication endpoints
│   ├── CoursesController.cs      # Course management
│   ├── EnrollmentsController.cs  # Student enrollments
│   ├── QuizzesController.cs      # Quiz management
│   └── AnalyticsController.cs    # Platform analytics
├── Models/                   # Domain models
├── DTOs/                     # Data Transfer Objects
├── Services/                 # Business logic services
├── Data/                     # Database context and initialization
├── frontend/                 # Angular application
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/       # UI components
│   │   │   ├── services/         # API services
│   │   │   ├── guards/           # Auth guards
│   │   │   ├── models/           # TypeScript interfaces
│   │   │   └── interceptors/     # HTTP interceptors
│   │   └── environments/     # Environment configs
│   ├── package.json
│   └── angular.json
├── appsettings.json          # Backend configuration
├── Program.cs                # Application entry point
└── start.sh                  # Startup script
```

## 🔐 Default Roles

The system creates three roles automatically:
- **Admin**: Full platform access
- **Instructor**: Can create and manage courses
- **Student**: Can enroll and learn

## 📚 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Courses
- `GET /api/courses` - Get all courses (with filters)
- `GET /api/courses/{id}` - Get course details
- `POST /api/courses` - Create course (Instructor)
- `PUT /api/courses/{id}` - Update course (Instructor)
- `DELETE /api/courses/{id}` - Delete course (Instructor)

### Enrollments
- `POST /api/enrollments` - Enroll in course
- `GET /api/enrollments/my-enrollments` - Get student's enrollments
- `POST /api/enrollments/{id}/lessons/{lessonId}/complete` - Mark lesson complete

### Quizzes
- `GET /api/quizzes/{id}` - Get quiz
- `POST /api/quizzes/submit` - Submit quiz answers
- `GET /api/quizzes/{id}/my-results` - Get quiz results

### Analytics (Admin)
- `GET /api/analytics/dashboard` - Platform statistics

See full API documentation at: `http://localhost:5000/swagger`

## 💾 Database

The application uses **SQLite** by default (no setup required). The database file `OnlineLearning.db` will be created automatically when you first run the backend.

To use SQL Server or PostgreSQL, update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=OnlineLearningDB;Trusted_Connection=True;"
  }
}
```

And update `Program.cs` to use `UseSqlServer()` instead of `UseSqlite()`.

## 🧪 Testing the Application

### 1. Register a New User

Frontend:
- Navigate to http://localhost:4200/register
- Fill in the registration form
- Select role: Student or Instructor

API:
```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "student@test.com",
    "password": "Test123!",
    "fullName": "Test Student",
    "role": "Student"
  }'
```

### 2. Login

Frontend:
- Navigate to http://localhost:4200/login
- Enter credentials

API:
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "student@test.com",
    "password": "Test123!"
  }'
```

### 3. Browse Courses

- Frontend: Navigate to http://localhost:4200/courses
- Use filters to search by category or level

## 🐳 Docker Support

Build and run with Docker Compose:

```bash
docker-compose up -d
```

## 📝 Configuration

### Backend Configuration (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=OnlineLearning.db"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "OnlineLearningPlatform",
    "Audience": "OnlineLearningPlatformUsers",
    "ExpiryInHours": 24
  }
}
```

### Frontend Configuration (`frontend/src/environments/environment.ts`)

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

## 🎨 Frontend Pages

- **Home** (`/`) - Landing page with featured courses
- **Courses** (`/courses`) - Browse all courses with filters
- **Course Details** (`/courses/:id`) - View course information and enroll
- **Login** (`/login`) - User authentication
- **Register** (`/register`) - User registration
- **Student Dashboard** (`/student/dashboard`) - View enrolled courses
- **Lesson Viewer** (`/learn/:enrollmentId/lesson/:lessonId`) - Watch video lessons
- **Quiz** (`/quiz/:quizId`) - Take quizzes
- **Instructor Dashboard** (`/instructor/dashboard`) - Manage courses
- **Admin Dashboard** (`/admin/dashboard`) - Platform statistics

## 🔒 Security

- JWT Bearer token authentication
- Role-based authorization (Admin, Instructor, Student)
- HTTP-only authentication
- CORS configured for localhost development
- Password requirements enforced

## 🐛 Troubleshooting

### Backend won't start
- Ensure .NET 7 SDK is installed: `dotnet --version`
- Check port 5000 is not in use: `lsof -i :5000`
- Delete `OnlineLearning.db` and restart to recreate database

### Frontend won't start
- Ensure Node.js 18+ is installed: `node --version`
- Delete `node_modules` and run `npm install` again
- Check port 4200 is not in use: `lsof -i :4200`

### CORS errors
- Ensure backend is running on port 5000
- Check `environment.ts` has correct API URL
- Verify CORS policy in `Program.cs` allows localhost:4200

## 📧 Contact

**Richard Sawanaka**  
Email: richardsawanaka@gmail.com

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

Built with ❤️ using ASP.NET Core 7 and Angular 17
