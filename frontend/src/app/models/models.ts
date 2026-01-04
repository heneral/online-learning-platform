export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  roles: string[];
  avatarUrl?: string;
}

export interface AuthResponse {
  user: User;
  token: string;
}

export interface AuthResponseDto {
  token: string;
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
  avatarUrl?: string;
  expiration: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface Category {
  id: number;
  name: string;
  description?: string;
  iconUrl?: string;
  courseCount: number;
}

export interface Course {
  id: number;
  title: string;
  description: string;
  thumbnailUrl?: string;
  price: number;
  isFree: boolean;
  level: string;
  language: string;
  durationInMinutes: number;
  isPublished: boolean;
  createdAt: string;
  instructorName: string;
  categoryName: string;
  enrollmentCount: number;
  lessonCount: number;
  lessons?: Lesson[];
  quizzes?: Quiz[];
  resources?: CourseResource[];
}

export interface CourseCreateDto {
  title: string;
  description: string;
  thumbnailUrl?: string;
  price: number;
  isFree: boolean;
  level: string;
  language: string;
  categoryId: number;
}

export interface Lesson {
  id: number;
  title: string;
  description?: string;
  orderIndex: number;
  videoUrl?: string;
  durationInSeconds: number;
  isFree: boolean;
  createdAt: string;
  courseId: number;
}

export interface Enrollment {
  id: number;
  enrolledAt: string;
  completedAt?: string;
  progressPercentage: number;
  isCompleted: boolean;
  studentId: string;
  courseId: number;
  course?: Course;
  lessonProgresses?: LessonProgress[];
}

export interface LessonProgress {
  id: number;
  isCompleted: boolean;
  watchTimeInSeconds: number;
  completedAt?: string;
  lastWatchedAt: string;
  enrollmentId: number;
  lessonId: number;
}

export interface Quiz {
  id: number;
  title: string;
  description?: string;
  passingScore: number;
  timeLimit: number;
  createdAt: string;
  courseId: number;
  questionCount: number;
  questions?: QuizQuestion[];
}

export interface QuizQuestion {
  id: number;
  questionText: string;
  questionType: string;
  points: number;
  orderIndex: number;
  answers?: QuizAnswer[];
}

export interface QuizAnswer {
  id: number;
  answerText: string;
  isCorrect?: boolean; // Not included for students
}

export interface QuizResult {
  id: number;
  score: number;
  totalPoints: number;
  percentage: number;
  passed: boolean;
  completedAt: string;
  timeSpentInSeconds: number;
  quizTitle: string;
}

export interface QuizSubmission {
  quizId: number;
  answers: QuizAnswerSubmission[];
  timeSpentInSeconds: number;
}

export interface QuizAnswerSubmission {
  questionId: number;
  selectedAnswerIds: number[];
}

export interface InstructorStats {
  totalCourses: number;
  totalStudents: number;
  totalRevenue: number;
  averageRating: number;
}

export interface CourseResource {
  id: number;
  title: string;
  fileUrl: string;
  fileType: string;
  fileSizeInBytes: number;
  uploadedAt: string;
}