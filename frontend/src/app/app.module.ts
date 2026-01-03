import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Interceptors
import { AuthInterceptor } from './interceptors/auth.interceptor';

// Components
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { CoursesComponent } from './components/courses/courses.component';
import { CourseDetailComponent } from './components/courses/course-detail/course-detail.component';
import { LessonViewerComponent } from './components/lessons/lesson-viewer/lesson-viewer.component';
import { QuizTakeComponent } from './components/quizzes/quiz-take/quiz-take.component';
import { StudentDashboardComponent } from './components/dashboards/student-dashboard/student-dashboard.component';
import { InstructorDashboardComponent } from './components/dashboards/instructor-dashboard/instructor-dashboard.component';
import { AdminDashboardComponent } from './components/dashboards/admin-dashboard/admin-dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    CoursesComponent,
    CourseDetailComponent,
    LessonViewerComponent,
    QuizTakeComponent,
    StudentDashboardComponent,
    InstructorDashboardComponent,
    AdminDashboardComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
