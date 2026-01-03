// Authentication Models
export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  fullName: string;
  role: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  userId: string;
  roles: string[];
}

// User Models
export interface User {
  id: string;
  email: string;
  fullName: string;
  roles: string[];
}

// Course Models
export interface Course {
  id: number;
  title: string;
  description: string;
  thumbnailUrl?: string;
  price: number;
  level: string;
  duration: number;
  enrollmentCount: number;
  rating: number;
  isPublished: boolean;
  createdAt: Date;
  instructorId: string;
  instructorName: string;
  categoryId: number;
  categoryName: string;
  lessons: Lesson[];
}

export interface CourseCreateDto {
  title: string;
  description: string;
  thumbnailUrl?: string;
  price: number;
  level: string;
  categoryId: number;
}

// Lesson Models
export interface Lesson {
  id: number;
  title: string;
  description?: string;
  orderIndex: number;
  videoUrl?: string;
  durationInSeconds: number;
  isFree: boolean;
  createdAt: Date;
  courseId: number;
}

export interface LessonCreateDto {
  title: string;
  description?: string;
  orderIndex: number;
  videoUrl?: string;
  durationInSeconds: number;
  isFree: boolean;
  courseId: number;
}

// Quiz Models
export interface Quiz {
  id: number;
  title: string;
  description?: string;
  passingScore: number;
  timeLimit: number;
  createdAt: Date;
  courseId: number;
  questions: QuizQuestion[];
}

export interface QuizQuestion {
  id: number;
  questionText: string;
  questionType: string;
  points: number;
  orderIndex: number;
  answers: QuizAnswer[];
}

export interface QuizAnswer {
  id: number;
  answerText: string;
  isCorrect: boolean;
}

export interface QuizSubmission {
  quizId: number;
  answers: {
    questionId: number;
    selectedAnswerId: number;
  }[];
  timeSpentInSeconds: number;
}

export interface QuizResult {
  id: number;
  score: number;
  totalPoints: number;
  passed: boolean;
  completedAt: Date;
  timeSpentInSeconds: number;
  quizId: number;
  studentId: string;
}

// Enrollment Models
export interface Enrollment {
  id: number;
  enrolledAt: Date;
  completedAt?: Date;
  progressPercentage: number;
  isCompleted: boolean;
  studentId: string;
  courseId: number;
  course: Course;
}

export interface LessonProgress {
  id: number;
  isCompleted: boolean;
  watchTimeInSeconds: number;
  completedAt?: Date;
  lastWatchedAt: Date;
  lessonId: number;
  lesson: Lesson;
}

// Category Models
export interface Category {
  id: number;
  name: string;
  description?: string;
  iconUrl?: string;
  courseCount: number;
}

// Analytics Models
export interface DashboardStats {
  totalStudents: number;
  totalInstructors: number;
  totalCourses: number;
  totalEnrollments: number;
  totalRevenue: number;
  recentEnrollments: Enrollment[];
  popularCourses: Course[];
}

export interface InstructorStats {
  totalCourses: number;
  totalStudents: number;
  totalRevenue: number;
  averageRating: number;
  courses: Course[];
}
