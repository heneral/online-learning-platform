import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Enrollment, LessonProgress } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {

  private apiUrl = 'api/enrollments';

  constructor(private http: HttpClient) { }

  getEnrollments(): Observable<Enrollment[]> {
    return this.http.get<Enrollment[]>(this.apiUrl);
  }

  getEnrollment(id: number): Observable<Enrollment> {
    return this.http.get<Enrollment>(`${this.apiUrl}/${id}`);
  }

  enroll(courseId: number): Observable<Enrollment> {
    return this.http.post<Enrollment>(this.apiUrl, { courseId });
  }

  updateProgress(enrollmentId: number, progress: LessonProgress[]): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${enrollmentId}/progress`, progress);
  }
}
