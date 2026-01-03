# Quick Start Guide

## 🚀 Start the Application

### Start Backend
```bash
cd /home/richardsawanaka/Documents/Workspace/online-learning-platform
snap run dotnet-sdk.dotnet run
```

Backend will be available at: **http://localhost:5000**

### Start Frontend
```bash
cd /home/richardsawanaka/Documents/Workspace/online-learning-platform/frontend
npm start
```

Frontend will be available at: **http://localhost:4200**

## 👤 Login Credentials

### Pre-seeded Test Accounts

**Admin:**
- Email: `admin@learningplatform.com`
- Password: `Admin123!`
- Access: Full platform management, analytics, user management

**Instructors:**
1. John Doe
   - Email: `john.doe@learningplatform.com`
   - Password: `Instructor123!`
   
2. Jane Smith
   - Email: `jane.smith@learningplatform.com`
   - Password: `Instructor123!`
   
Access: Create/manage courses, create lessons, quizzes, view analytics

**Students:**
1. Alice Johnson
   - Email: `student1@example.com`
   - Password: `Student123!`
   
2. Bob Williams
   - Email: `student2@example.com`
   - Password: `Student123!`
   
3. Charlie Brown
   - Email: `student3@example.com`
   - Password: `Student123!`
   
Access: Browse courses, enroll, watch lessons, take quizzes, track progress

## 📚 Sample Data

The database is pre-seeded with:
- **5 Courses** covering Programming, Design, Marketing, and Data Science
- **4 Lessons** in the ASP.NET Core course
- **1 Quiz** with 2 questions
- **5 Categories**: Programming, Design, Business, Marketing, Data Science

## 🎯 Quick Test Flow

1. **Login as Student** (student1@example.com)
2. Browse the **5 available courses** on the home page
3. Click on "Complete ASP.NET Core Masterclass"
4. **Enroll** in the course
5. Go to **Student Dashboard** to see enrolled courses
6. Watch lessons and track progress
7. Take the quiz

## 🔗 API Documentation

Swagger UI: **http://localhost:5000**

The Swagger interface allows you to:
- Test all API endpoints
- View request/response models
- Authenticate with JWT tokens
- Explore all available operations

## 🎨 Design System

The UI uses a modern design with:
- **Primary Color**: Indigo (#4f46e5)
- **Secondary Color**: Sky Blue (#0ea5e9)
- **Success Color**: Green (#10b981)
- Clean, solid color palette (no gradients)
- Smooth animations and transitions
- Professional card-based layouts

## 📱 Features to Test

### As Student:
- ✅ Browse course catalog
- ✅ Search and filter courses
- ✅ View course details with lessons
- ✅ Enroll in free and paid courses
- ✅ Watch video lessons
- ✅ Track lesson progress
- ✅ Take quizzes and see results
- ✅ View personal dashboard with progress

### As Instructor:
- ✅ Create new courses
- ✅ Add lessons to courses
- ✅ Create quizzes with questions
- ✅ View course analytics
- ✅ Manage enrolled students
- ✅ Publish/unpublish courses

### As Admin:
- ✅ View platform-wide analytics
- ✅ Manage all courses and users
- ✅ Monitor enrollments
- ✅ Access complete dashboard

## 🛠 Troubleshooting

### Backend won't start:
```bash
# Delete the database and restart
rm OnlineLearning.db
snap run dotnet-sdk.dotnet run
```

### Frontend shows errors:
```bash
# Reinstall dependencies
cd frontend
rm -rf node_modules package-lock.json
npm install
```

### Can't login:
- Make sure backend is running on port 5000
- Check browser console for CORS errors
- Verify you're using the correct credentials
- Try: student1@example.com / Student123!

## 📂 Project Structure

```
online-learning-platform/
├── Controllers/          # API Controllers
├── Models/              # Database Models
├── Services/            # Business Logic
├── Data/                # Database Context & Seed Data
├── DTOs/                # Data Transfer Objects
├── frontend/            # Angular Application
│   └── src/
│       ├── app/
│       │   ├── components/
│       │   ├── services/
│       │   ├── guards/
│       │   └── models/
│       └── styles.css   # Global Styles
├── OnlineLearning.db    # SQLite Database
├── README.md           # Full Documentation
└── QUICK_START.md      # This File
```

## 🎓 Learning Resources

- Full API docs: [README.md](README.md)
- Architecture details: See README Architecture section
- Sample API calls: See README Usage Guide section
- Database schema: Check Models/ directory

---

**Built with ❤️ by Richard Sawanaka**
Email: richardsawanaka@gmail.com
