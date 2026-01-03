import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Enrollment, LessonProgress } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {
  private apiUrl = `${environment.apiUrl}/enrollments`;

  constructor(private http: HttpClient) {}

  enroll(courseId: number): Observable<Enrollment> {
    return this.http.post<Enrollment>(this.apiUrl, { courseId });
  }

  getMyEnrollments(): Observable<Enrollment[]> {
    return this.http.get<Enrollment[]>(`${this.apiUrl}/my-enrollments`);
  }

  getEnrollment(enrollmentId: number): Observable<Enrollment> {
    return this.http.get<Enrollment>(`${this.apiUrl}/${enrollmentId}`);
  }

  updateLessonProgress(enrollmentId: number, lessonId: number, watchTimeInSeconds: number): Observable<void> {
    return this.http.post<void>(
      `${this.apiUrl}/${enrollmentId}/lessons/${lessonId}/progress`,
      { watchTimeInSeconds }
    );
  }

  completLesson(enrollmentId: number, lessonId: number): Observable<void> {
    return this.http.post<void>(
      `${this.apiUrl}/${enrollmentId}/lessons/${lessonId}/complete`,
      {}
    );
  }

  getLessonProgress(enrollmentId: number): Observable<LessonProgress[]> {
    return this.http.get<LessonProgress[]>(`${this.apiUrl}/${enrollmentId}/progress`);
  }
}
