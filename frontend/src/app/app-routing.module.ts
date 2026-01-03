import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { CoursesComponent } from './components/courses/courses.component';
import { CourseDetailComponent } from './components/courses/course-detail/course-detail.component';
import { LessonViewerComponent } from './components/lessons/lesson-viewer/lesson-viewer.component';
import { QuizTakeComponent } from './components/quizzes/quiz-take/quiz-take.component';
import { StudentDashboardComponent } from './components/dashboards/student-dashboard/student-dashboard.component';
import { InstructorDashboardComponent } from './components/dashboards/instructor-dashboard/instructor-dashboard.component';
import { AdminDashboardComponent } from './components/dashboards/admin-dashboard/admin-dashboard.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'courses', component: CoursesComponent },
  { path: 'courses/:id', component: CourseDetailComponent },
  { 
    path: 'learn/:enrollmentId/lesson/:lessonId', 
    component: LessonViewerComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: 'quiz/:quizId', 
    component: QuizTakeComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: 'student/dashboard', 
    component: StudentDashboardComponent,
    canActivate: [AuthGuard],
    data: { role: 'Student' }
  },
  { 
    path: 'instructor/dashboard', 
    component: InstructorDashboardComponent,
    canActivate: [AuthGuard],
    data: { role: 'Instructor' }
  },
  { 
    path: 'admin/dashboard', 
    component: AdminDashboardComponent,
    canActivate: [AuthGuard],
    data: { role: 'Admin' }
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
