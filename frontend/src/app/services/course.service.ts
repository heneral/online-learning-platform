import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course, CourseCreateDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private apiUrl = 'api/courses';

  constructor(private http: HttpClient) { }

  getCourses(categoryId?: number, searchTerm?: string, level?: string): Observable<Course[]> {
    let params = new HttpParams();
    if (categoryId) params = params.set('categoryId', categoryId.toString());
    if (searchTerm) params = params.set('searchTerm', searchTerm);
    if (level) params = params.set('level', level);
    return this.http.get<Course[]>(this.apiUrl, { params });
  }

  getInstructorCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.apiUrl}/instructor`);
  }

  getCourse(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`);
  }

  createCourse(course: CourseCreateDto): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, course);
  }

  updateCourse(id: number, course: Partial<CourseCreateDto>): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/${id}`, course);
  }

  deleteCourse(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
