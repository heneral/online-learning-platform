# Quick Start Guide

## Initial Setup (First Time)

1. **Install Prerequisites**
   - .NET 7 SDK
   - SQL Server or SQL Server Express

2. **Clone and Restore**
   ```bash
   git clone <repository-url>
   cd online-learning-platform
   dotnet restore
   ```

3. **Configure Database**
   
   Edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=OnlineLearningDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

4. **Create Database**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run Application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   
   Open browser: `https://localhost:5001`

## Testing the API

### 1. Register a Student
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "student@test.com",
    "password": "Test123!",
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "2000-01-01",
    "role": "Student"
  }'
```

### 2. Register an Instructor
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "instructor@test.com",
    "password": "Test123!",
    "firstName": "Jane",
    "lastName": "Smith",
    "dateOfBirth": "1985-05-15",
    "role": "Instructor"
  }'
```

### 3. Login
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "instructor@test.com",
    "password": "Test123!"
  }'
```

Copy the `token` from the response.

### 4. Create a Course (as Instructor)
```bash
curl -X POST https://localhost:5001/api/courses \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
    "title": "Introduction to ASP.NET Core",
    "description": "Learn ASP.NET Core from scratch",
    "price": 49.99,
    "isFree": false,
    "level": "Beginner",
    "language": "English",
    "categoryId": 1
  }'
```

### 5. Browse Courses (as Student)
```bash
curl https://localhost:5001/api/courses
```

### 6. Enroll in Course
```bash
curl -X POST https://localhost:5001/api/enrollments \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_STUDENT_TOKEN" \
  -d '{
    "courseId": 1
  }'
```

## Common Commands

### Database Management
```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Drop database
dotnet ef database drop
```

### Build and Run
```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Run project
dotnet run

# Run with watch (auto-reload)
dotnet watch run

# Publish for production
dotnet publish -c Release
```

### Docker
```bash
# Build and run with Docker Compose
docker-compose up -d

# View logs
docker-compose logs -f web

# Stop containers
docker-compose down

# Rebuild containers
docker-compose up -d --build
```

## Troubleshooting

### Issue: Database connection failed
**Solution**: Check SQL Server is running and connection string is correct

### Issue: Migration failed
**Solution**: Delete `Migrations` folder, drop database, and recreate migrations:
```bash
dotnet ef database drop
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Issue: JWT token invalid
**Solution**: Ensure JWT Key in `appsettings.json` is at least 32 characters

### Issue: Port already in use
**Solution**: Change port in `Properties/launchSettings.json`

## Default Categories

The database seeds with these categories:
1. Programming
2. Design
3. Business
4. Marketing
5. Data Science

## User Roles

- **Student**: Can browse, enroll, watch lessons, take quizzes
- **Instructor**: Can create courses, lessons, quizzes
- **Admin**: Can view analytics, manage platform

## Next Steps

1. Create courses as an Instructor
2. Add lessons and quizzes to courses
3. Enroll as a Student
4. Track progress
5. Take quizzes
6. View analytics as Admin

For detailed documentation, see [README.md](README.md)
