# Getting Started - Test Credentials

## 🚀 Quick Setup

### 1. Prerequisites Installed?
- [ ] .NET 7 SDK
- [ ] SQL Server or SQL Server Express
- [ ] Git (optional)

### 2. Initial Setup

Run the automated setup script:

**Linux/Mac:**
```bash
./setup.sh
```

**Windows:**
```cmd
setup.bat
```

Or manually:
```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

### 3. Seed Test Data (Optional)

To automatically create test users and courses, set environment variable before running:

**Linux/Mac:**
```bash
export SEED_TEST_DATA=true
dotnet run
```

**Windows (PowerShell):**
```powershell
$env:SEED_TEST_DATA="true"
dotnet run
```

**Windows (CMD):**
```cmd
set SEED_TEST_DATA=true
dotnet run
```

## 🔑 Test Account Credentials

If you seeded test data, these accounts are available:

### Admin Account
```
Email: admin@learningplatform.com
Password: Admin123!
Role: Admin
```
**Can do:** View analytics, manage platform

### Instructor Accounts

**Instructor 1:**
```
Email: john.doe@learningplatform.com
Password: Instructor123!
Role: Instructor
```

**Instructor 2:**
```
Email: jane.smith@learningplatform.com
Password: Instructor123!
Role: Instructor
```
**Can do:** Create courses, lessons, quizzes

### Student Accounts

**Student 1:**
```
Email: student1@example.com
Password: Student123!
Role: Student
Name: Alice Johnson
```

**Student 2:**
```
Email: student2@example.com
Password: Student123!
Role: Student
Name: Bob Williams
```

**Student 3:**
```
Email: student3@example.com
Password: Student123!
Role: Student
Name: Charlie Brown
```
**Can do:** Enroll in courses, watch lessons, take quizzes

## 📚 Pre-seeded Content

### Categories (Always Seeded)
1. Programming
2. Design
3. Business
4. Marketing
5. Data Science

### Courses (If Test Data Seeded)
1. **Complete ASP.NET Core Masterclass** - Paid ($99.99)
   - Instructor: John Doe
   - Level: Intermediate
   - 4 Lessons included
   - 1 Quiz included

2. **Introduction to Web Design** - Paid ($49.99)
   - Instructor: Jane Smith
   - Level: Beginner

3. **C# Programming Fundamentals** - FREE
   - Instructor: John Doe
   - Level: Beginner

4. **Digital Marketing Essentials** - Paid ($79.99)
   - Instructor: Jane Smith
   - Level: Intermediate

5. **Python for Data Science** - Paid ($129.99)
   - Instructor: John Doe
   - Level: Advanced

## 🎯 Testing Scenarios

### Scenario 1: Student Journey
1. Login as Student 1
2. Browse courses: `GET /api/courses`
3. View course details: `GET /api/courses/1`
4. Enroll: `POST /api/enrollments` with `{"courseId": 1}`
5. View my enrollments: `GET /api/enrollments`
6. Watch lesson: `POST /api/enrollments/lesson-progress`
7. Take quiz: `GET /api/quizzes/1` then `POST /api/quizzes/submit`
8. Check progress: `GET /api/enrollments/progress/1`

### Scenario 2: Instructor Journey
1. Login as Instructor 1
2. View my courses: `GET /api/courses/instructor/my-courses`
3. Create new course: `POST /api/courses`
4. Add quiz: `POST /api/quizzes`
5. Publish course: `PUT /api/courses/{id}` with `{"isPublished": true}`

### Scenario 3: Admin Journey
1. Login as Admin
2. View analytics: `GET /api/analytics`
3. Browse all categories: `GET /api/categories`
4. Monitor course performance

## 🌐 API Access

### Swagger UI (Recommended for Testing)
```
URL: https://localhost:5001
```

1. Click "Authorize" button at top right
2. Enter: `Bearer YOUR_TOKEN_HERE`
3. Test endpoints directly

### Postman Collection

Import these base URLs:
- **Base URL**: `https://localhost:5001`
- **Alt URL**: `http://localhost:5000`

### cURL Examples

**Login:**
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "student1@example.com",
    "password": "Student123!"
  }'
```

**Browse Courses:**
```bash
curl https://localhost:5001/api/courses
```

**Enroll (with token):**
```bash
curl -X POST https://localhost:5001/api/enrollments \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{"courseId": 1}'
```

## 🔧 Manual User Creation (Without Seed Data)

If you didn't seed test data, create users via API:

### Register Admin
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "youradmin@example.com",
    "password": "YourPassword123!",
    "firstName": "Your",
    "lastName": "Name",
    "dateOfBirth": "1990-01-01",
    "role": "Admin"
  }'
```

### Register Instructor
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "yourinstructor@example.com",
    "password": "YourPassword123!",
    "firstName": "Your",
    "lastName": "Name",
    "dateOfBirth": "1985-05-15",
    "role": "Instructor"
  }'
```

### Register Student
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "yourstudent@example.com",
    "password": "YourPassword123!",
    "firstName": "Your",
    "lastName": "Name",
    "dateOfBirth": "2000-01-01",
    "role": "Student"
  }'
```

## 📝 Password Requirements

All passwords must have:
- At least 6 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one digit

## 🎓 What to Try

### For Students:
- [x] Browse free and paid courses
- [x] Enroll in a free course (C# Fundamentals)
- [x] Watch first two lessons (they're free!)
- [x] Update lesson progress
- [x] Take the ASP.NET Core quiz
- [x] Check your progress percentage
- [x] View your enrollments

### For Instructors:
- [x] View your existing courses
- [x] Create a new course
- [x] Add lessons to course
- [x] Create a quiz with multiple questions
- [x] Publish the course
- [x] Update course details

### For Admins:
- [x] View platform analytics
- [x] See total students/instructors
- [x] View popular courses
- [x] Check category statistics

## 🐛 Troubleshooting

### "Cannot connect to database"
- Ensure SQL Server is running
- Check connection string in `appsettings.json`
- Run: `dotnet ef database update`

### "Unauthorized" Error
- Make sure you're logged in
- Check token hasn't expired (24 hour default)
- Verify Authorization header: `Bearer YOUR_TOKEN`

### "Forbidden" Error
- Check you're using correct role account
- Students can't create courses
- Only instructors can add quizzes

### Can't Login with Test Accounts
- Ensure you ran with `SEED_TEST_DATA=true`
- Check database has user records
- Try creating account manually via register endpoint

## 📞 Need Help?

Check these files:
1. **README.md** - Full documentation
2. **QUICKSTART.md** - Quick start guide
3. **API_TESTING.md** - API examples
4. **PROJECT_SUMMARY.md** - Project overview

## 🎉 You're Ready!

Open Swagger UI at `https://localhost:5001` and start testing!

**Happy Learning! 🚀**
