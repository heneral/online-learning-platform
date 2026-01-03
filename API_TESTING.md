# API Testing Examples

## Using Swagger UI

Navigate to `https://localhost:5001` after running the application.

1. Click "Authorize" button at the top
2. Enter: `Bearer YOUR_TOKEN_HERE`
3. Test endpoints directly in the browser

## Using Postman

### 1. Create Environment Variables

Create a new environment with these variables:
- `baseUrl`: `https://localhost:5001`
- `token`: (will be set after login)
- `studentToken`: (will be set after student login)
- `instructorToken`: (will be set after instructor login)

### 2. Collection Structure

```
Online Learning Platform/
├── Auth/
│   ├── Register Student
│   ├── Register Instructor
│   ├── Register Admin
│   └── Login
├── Courses/
│   ├── Get All Courses
│   ├── Get Course by ID
│   ├── Get Courses by Category
│   ├── Create Course (Instructor)
│   ├── Update Course (Instructor)
│   ├── Delete Course (Instructor)
│   └── Get My Courses (Instructor)
├── Enrollments/
│   ├── Enroll in Course (Student)
│   ├── Get My Enrollments (Student)
│   ├── Get Course Progress (Student)
│   └── Update Lesson Progress (Student)
├── Quizzes/
│   ├── Get Quiz
│   ├── Create Quiz (Instructor)
│   ├── Submit Quiz (Student)
│   └── Get Quiz Results (Student)
├── Categories/
│   ├── Get All Categories
│   └── Get Category by ID
└── Analytics/
    └── Get Analytics (Admin)
```

## Sample Requests

### Authentication

#### Register Student
```http
POST {{baseUrl}}/api/auth/register
Content-Type: application/json

{
  "email": "student@example.com",
  "password": "Student123!",
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "2000-01-01",
  "role": "Student"
}
```

#### Login
```http
POST {{baseUrl}}/api/auth/login
Content-Type: application/json

{
  "email": "student@example.com",
  "password": "Student123!"
}
```

**Save token from response:**
```javascript
// In Postman Tests tab:
pm.environment.set("token", pm.response.json().token);
```

### Courses

#### Get All Courses
```http
GET {{baseUrl}}/api/courses
```

#### Create Course (Instructor)
```http
POST {{baseUrl}}/api/courses
Authorization: Bearer {{instructorToken}}
Content-Type: application/json

{
  "title": "Complete ASP.NET Core Masterclass",
  "description": "Learn ASP.NET Core from beginner to advanced level",
  "thumbnailUrl": "https://example.com/thumbnail.jpg",
  "price": 99.99,
  "isFree": false,
  "level": "Intermediate",
  "language": "English",
  "categoryId": 1
}
```

#### Update Course
```http
PUT {{baseUrl}}/api/courses/1
Authorization: Bearer {{instructorToken}}
Content-Type: application/json

{
  "title": "Complete ASP.NET Core Masterclass - Updated",
  "isPublished": true
}
```

### Enrollments

#### Enroll in Course
```http
POST {{baseUrl}}/api/enrollments
Authorization: Bearer {{studentToken}}
Content-Type: application/json

{
  "courseId": 1
}
```

#### Update Lesson Progress
```http
POST {{baseUrl}}/api/enrollments/lesson-progress
Authorization: Bearer {{studentToken}}
Content-Type: application/json

{
  "enrollmentId": 1,
  "lessonId": 1,
  "watchTimeInSeconds": 180,
  "isCompleted": true
}
```

### Quizzes

#### Create Quiz
```http
POST {{baseUrl}}/api/quizzes
Authorization: Bearer {{instructorToken}}
Content-Type: application/json

{
  "title": "ASP.NET Core Fundamentals Quiz",
  "description": "Test your knowledge of ASP.NET Core basics",
  "courseId": 1,
  "passingScore": 70,
  "timeLimit": 30,
  "questions": [
    {
      "questionText": "What is ASP.NET Core?",
      "questionType": "MultipleChoice",
      "points": 1,
      "orderIndex": 1,
      "answers": [
        {
          "answerText": "A web framework",
          "isCorrect": true
        },
        {
          "answerText": "A database",
          "isCorrect": false
        },
        {
          "answerText": "An operating system",
          "isCorrect": false
        }
      ]
    },
    {
      "questionText": "ASP.NET Core is cross-platform",
      "questionType": "TrueFalse",
      "points": 1,
      "orderIndex": 2,
      "answers": [
        {
          "answerText": "True",
          "isCorrect": true
        },
        {
          "answerText": "False",
          "isCorrect": false
        }
      ]
    }
  ]
}
```

#### Submit Quiz
```http
POST {{baseUrl}}/api/quizzes/submit
Authorization: Bearer {{studentToken}}
Content-Type: application/json

{
  "quizId": 1,
  "answers": [
    {
      "questionId": 1,
      "selectedAnswerIds": [1]
    },
    {
      "questionId": 2,
      "selectedAnswerIds": [3]
    }
  ],
  "timeSpentInSeconds": 300
}
```

## Testing Workflow

### Complete User Journey - Student

1. **Register** as a student
2. **Login** and save token
3. **Browse courses** - GET /api/courses
4. **View course details** - GET /api/courses/1
5. **Enroll in course** - POST /api/enrollments
6. **View my enrollments** - GET /api/enrollments
7. **Start watching lesson** - POST /api/enrollments/lesson-progress
8. **Complete lesson** - POST /api/enrollments/lesson-progress (isCompleted: true)
9. **View progress** - GET /api/enrollments/progress/1
10. **Take quiz** - GET /api/quizzes/1
11. **Submit quiz** - POST /api/quizzes/submit
12. **View results** - GET /api/quizzes/results/course/1

### Complete User Journey - Instructor

1. **Register** as an instructor
2. **Login** and save token
3. **Create course** - POST /api/courses
4. **Add lessons** (implement LessonsController or add directly to DB)
5. **Create quiz** - POST /api/quizzes
6. **Publish course** - PUT /api/courses/1 (isPublished: true)
7. **View my courses** - GET /api/courses/instructor/my-courses

### Complete User Journey - Admin

1. **Register** as admin (or promote existing user in database)
2. **Login** and save token
3. **View analytics** - GET /api/analytics
4. **View all categories** - GET /api/categories

## Expected Response Codes

- `200 OK` - Successful GET/PUT requests
- `201 Created` - Successful POST requests (resource created)
- `204 No Content` - Successful DELETE requests
- `400 Bad Request` - Invalid input data
- `401 Unauthorized` - Missing or invalid token
- `403 Forbidden` - Valid token but insufficient permissions
- `404 Not Found` - Resource not found

## Error Response Format

```json
{
  "message": "Error description here"
}
```

## Tips

1. **Always check token expiration** - Tokens expire after 24 hours (configurable)
2. **Use correct role tokens** - Student endpoints need student token, etc.
3. **Create categories first** - Database seeds 5 categories automatically
4. **Publish courses** - Only published courses appear in student course list
5. **Enroll before progress tracking** - Must be enrolled to track lesson progress

## Postman Collection Export

You can import the complete collection by creating a JSON file with all these requests. Export it from Postman for sharing with your team.
