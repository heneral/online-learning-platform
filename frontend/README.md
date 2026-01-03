# Online Learning Platform - Angular Frontend

Angular 17 frontend application for the Online Learning Platform.

## Features

- **Authentication**: Login and registration with JWT tokens
- **Course Browsing**: Search and filter courses by category and level
- **Student Features**:
  - Enroll in courses
  - Watch video lessons
  - Take quizzes
  - Track learning progress
  - View certificates
- **Instructor Features**:
  - Create and manage courses
  - Upload lessons and videos
  - Create quizzes
  - View analytics
- **Admin Features**:
  - Dashboard with platform statistics
  - User management
  - Course moderation

## Prerequisites

- Node.js 18+ and npm
- Angular CLI 17+

## Installation

```bash
cd frontend
npm install
```

## Configuration

Update the API URL in `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

## Development

```bash
npm start
```

Navigate to `http://localhost:4200/`

## Build

```bash
npm run build
```

Build artifacts will be stored in the `dist/` directory.

## Project Structure

```
src/
├── app/
│   ├── components/          # UI components
│   │   ├── auth/           # Login, Register
│   │   ├── courses/        # Course listing, details
│   │   ├── dashboards/     # Student, Instructor, Admin dashboards
│   │   ├── lessons/        # Video player
│   │   ├── quizzes/        # Quiz taking
│   │   ├── navbar/         # Navigation
│   │   └── home/           # Landing page
│   ├── services/           # API services
│   ├── guards/             # Auth guards
│   ├── interceptors/       # HTTP interceptors
│   ├── models/             # TypeScript interfaces
│   └── app.module.ts       # Main module
├── environments/           # Environment configs
├── assets/                 # Static assets
└── styles.css             # Global styles
```

## Contact

Richard Sawanaka - richardsawanaka@gmail.com
