# Online Learning Platform - ASP.NET Core

A comprehensive online learning platform built with ASP.NET Core 7, featuring role-based access control, course management, video lessons, quizzes, progress tracking, and certificates.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Usage Guide](#usage-guide)
- [Project Structure](#project-structure)
- [Docker Deployment](#docker-deployment)
- [Security](#security)
- [Future Enhancements](#future-enhancements)

## ✨ Features

### User Roles
- **Student**: Browse courses, enroll, watch lessons, take quizzes, track progress
- **Instructor**: Create courses, upload videos, manage content, create quizzes, view analytics
- **Admin**: Manage users, courses, categories, view platform-wide analytics

### Core Functionality
- 📚 Course catalog with categories and search
- 🎥 Video lessons with progress tracking
- 📝 Interactive quizzes and assessments
- 📊 Progress tracking and completion certificates
- 🏆 Real-time enrollment statistics
- 📈 Admin analytics dashboard
- 🔒 JWT-based authentication
- 🎯 Role-based authorization

## 🛠 Tech Stack

- **Backend**: ASP.NET Core 7 Web API
- **Database**: SQL Server (PostgreSQL compatible)
- **ORM**: Entity Framework Core 7
- **Authentication**: ASP.NET Identity + JWT
- **Storage**: Azure Blob Storage (for videos)
- **Documentation**: Swagger/OpenAPI
- **Containerization**: Docker

## 🏗 Architecture

```
┌─────────────┐         ┌──────────────────┐         ┌─────────────┐
│   Client    │ ◄─────► │  ASP.NET Core    │ ◄─────► │  SQL Server │
│ (Frontend)  │   API   │  Web API         │   EF    │  Database   │
└─────────────┘         └──────────────────┘         └─────────────┘
                              │      │
                              │      │
                   ┌──────────┘      └──────────┐
                   │                             │
                   ▼                             ▼
           ┌─────────────┐             ┌─────────────┐
           │   Azure     │             │    Admin    │
           │   Blob      │             │  Analytics  │
           │   Storage   │             │  Dashboard  │
           └─────────────┘             └─────────────┘
```

### Layers
1. **Controllers**: API endpoints handling HTTP requests
2. **Services**: Business logic and data manipulation
3. **Data**: Entity Framework DbContext and models
4. **DTOs**: Data Transfer Objects for API communication

## 📋 Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server 2019+](https://www.microsoft.com/sql-server) or [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Azure Storage Emulator](https://learn.microsoft.com/azure/storage/common/storage-use-emulator) or Azure Storage Account (optional for video storage)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (optional, for containerized deployment)

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/online-learning-platform.git
cd online-learning-platform
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Update Connection String

Edit `appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OnlineLearningDB;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;"
  }
}
```

**For Windows Authentication:**
```json
"DefaultConnection": "Server=.;Database=OnlineLearningDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

**For PostgreSQL:**
```json
"DefaultConnection": "Host=localhost;Database=OnlineLearningDB;Username=postgres;Password=yourpassword"
```

## 🗄 Database Setup

### Create and Apply Migrations

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migration to database
dotnet ef database update
```

This will:
- Create the database
- Create all tables
- Seed initial categories
- Set up ASP.NET Identity tables

## ⚙ Configuration

### JWT Settings

Update JWT configuration in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "OnlineLearningPlatform",
    "Audience": "OnlineLearningPlatformUsers",
    "ExpiryInHours": 24
  }
}
```

### Azure Storage (Optional)

For video storage, configure Azure Blob Storage:

```json
{
  "AzureStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=yourkey;EndpointSuffix=core.windows.net",
    "ContainerName": "course-videos"
  }
}
```

**For local development**, use Azure Storage Emulator:
```json
"ConnectionString": "UseDevelopmentStorage=true"
```

## ▶ Running the Application

### Using .NET CLI

```bash
dotnet run
```

### Using Visual Studio

1. Open `OnlineLearningPlatform.sln`
2. Press F5 or click "Run"

### Using VS Code

1. Open the project folder
2. Press F5 or use "Run > Start Debugging"

The API will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger UI**: `https://localhost:5001` (root URL)

## 📖 API Documentation

Access the interactive Swagger documentation at `https://localhost:5001` after running the application.

### Key Endpoints

#### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

#### Courses
- `GET /api/courses` - Get all published courses
- `GET /api/courses/{id}` - Get course details
- `GET /api/courses/category/{categoryId}` - Get courses by category
- `POST /api/courses` - Create course (Instructor only)
- `PUT /api/courses/{id}` - Update course (Instructor only)
- `DELETE /api/courses/{id}` - Delete course (Instructor only)
- `GET /api/courses/instructor/my-courses` - Get instructor's courses

#### Enrollments
- `POST /api/enrollments` - Enroll in course (Student only)
- `GET /api/enrollments` - Get student's enrollments
- `GET /api/enrollments/progress/{courseId}` - Get course progress
- `POST /api/enrollments/lesson-progress` - Update lesson progress

#### Quizzes
- `GET /api/quizzes/{id}` - Get quiz details
- `POST /api/quizzes` - Create quiz (Instructor only)
- `POST /api/quizzes/submit` - Submit quiz answers (Student only)
- `GET /api/quizzes/results/course/{courseId}` - Get quiz results

#### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category details

#### Analytics
- `GET /api/analytics` - Get platform analytics (Admin only)

## 📘 Usage Guide

### 1. Register Users

**Register a Student:**
```bash
POST /api/auth/register
{
  "email": "student@example.com",
  "password": "Student123!",
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "2000-01-01",
  "role": "Student"
}
```

**Register an Instructor:**
```bash
POST /api/auth/register
{
  "email": "instructor@example.com",
  "password": "Instructor123!",
  "firstName": "Jane",
  "lastName": "Smith",
  "dateOfBirth": "1985-05-15",
  "role": "Instructor"
}
```

### Sample Users (Pre-seeded)

The database comes with pre-seeded test users:

**Admin:**
- Email: `admin@learningplatform.com`
- Password: `Admin123!`

**Instructors:**
- Email: `john.doe@learningplatform.com` | Password: `Instructor123!`
- Email: `jane.smith@learningplatform.com` | Password: `Instructor123!`

**Students:**
- Email: `student1@example.com` | Password: `Student123!`
- Email: `student2@example.com` | Password: `Student123!`
- Email: `student3@example.com` | Password: `Student123!`

### 2. Login and Get Token

```bash
POST /api/auth/login
{
  "email": "student@example.com",
  "password": "Student123!"
}
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": "user-id-here",
  "email": "student@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "roles": ["Student"],
  "expiration": "2026-01-05T12:00:00Z"
}
```

### 3. Use Token in Requests

Add the token to the Authorization header:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 4. Browse and Enroll in Courses

```bash
# Get all courses
GET /api/courses

# Get course details
GET /api/courses/1

# Enroll in course (Student with token)
POST /api/enrollments
{
  "courseId": 1
}
```

### 5. Track Progress

```bash
# Update lesson progress
POST /api/enrollments/lesson-progress
{
  "enrollmentId": 1,
  "lessonId": 1,
  "watchTimeInSeconds": 180,
  "isCompleted": true
}

# Get course progress
GET /api/enrollments/progress/1
```

### 6. Take Quizzes

```bash
# Get quiz
GET /api/quizzes/1

# Submit quiz
POST /api/quizzes/submit
{
  "quizId": 1,
  "answers": [
    {
      "questionId": 1,
      "selectedAnswerIds": [1, 3]
    }
  ],
  "timeSpentInSeconds": 300
}
```

## 📁 Project Structure

```
OnlineLearningPlatform/
│
├── Controllers/              # API Controllers
│   ├── AuthController.cs
│   ├── CoursesController.cs
│   ├── QuizzesController.cs
│   ├── EnrollmentsController.cs
│   ├── CategoriesController.cs
│   └── AnalyticsController.cs
│
├── Models/                   # Domain Models
│   ├── ApplicationUser.cs
│   ├── Course.cs
│   ├── Lesson.cs
│   ├── Quiz.cs
│   ├── QuizQuestion.cs
│   ├── QuizAnswer.cs
│   ├── QuizResult.cs
│   ├── Enrollment.cs
│   ├── LessonProgress.cs
│   ├── Certificate.cs
│   ├── Category.cs
│   └── CourseResource.cs
│
├── DTOs/                     # Data Transfer Objects
│   ├── AuthDtos.cs
│   ├── CourseDtos.cs
│   ├── LessonDtos.cs
│   ├── QuizDtos.cs
│   ├── EnrollmentDtos.cs
│   └── CommonDtos.cs
│
├── Services/                 # Business Logic
│   ├── CourseService.cs
│   ├── QuizService.cs
│   ├── EnrollmentService.cs
│   └── VideoStorageService.cs
│
├── Data/                     # Database Context
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│
├── appsettings.json          # Configuration
├── appsettings.Development.json
├── Program.cs                # Application Entry Point
├── Dockerfile                # Docker Configuration
├── docker-compose.yml        # Docker Compose
└── README.md                 # This File
```

## 🐳 Docker Deployment

### Build and Run with Docker Compose

```bash
# Build and start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

This will start:
- Web API on port 5000
- SQL Server on port 1433

### Build Docker Image Only

```bash
docker build -t online-learning-platform .
docker run -p 5000:80 online-learning-platform
```

## 🔒 Security

### Implemented Security Features

1. **JWT Authentication**: Token-based authentication with expiration
2. **Role-Based Authorization**: Three distinct roles (Student, Instructor, Admin)
3. **Password Requirements**: Strong password policy enforced
4. **HTTPS**: Redirect HTTP to HTTPS in production
5. **CORS**: Configurable cross-origin resource sharing
6. **Input Validation**: Data annotations on DTOs
7. **SQL Injection Protection**: Entity Framework parameterized queries

### Best Practices

- Store sensitive configuration in environment variables or Azure Key Vault
- Rotate JWT secret keys regularly
- Use HTTPS in production
- Implement rate limiting for API endpoints
- Enable logging and monitoring
- Regular security audits

## 🚀 Future Enhancements

### Planned Features
- [ ] Certificate generation (PDF)
- [ ] Discussion forums for courses
- [ ] Real-time notifications (SignalR)
- [ ] Payment integration (Stripe/PayPal)
- [ ] Course reviews and ratings
- [ ] Advanced search and filtering
- [ ] Email notifications
- [ ] Gamification (badges, points)
- [ ] Live streaming lessons
- [ ] Mobile app integration
- [ ] Multi-language support
- [ ] Content recommendation engine

### Performance Improvements
- [ ] Implement caching (Redis)
- [ ] Add pagination to all list endpoints
- [ ] Optimize database queries
- [ ] Implement CDN for video delivery
- [ ] Add background jobs (Hangfire)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For issues, questions, or contributions:
- Create an issue on GitHub
- Email: richardsawanaka@gmail.com

## 📚 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [JWT Authentication](https://jwt.io/introduction)
- [Azure Blob Storage](https://docs.microsoft.com/azure/storage/blobs/)
- [Swagger/OpenAPI](https://swagger.io/docs/)

---

**Built with ❤️ using ASP.NET Core**
