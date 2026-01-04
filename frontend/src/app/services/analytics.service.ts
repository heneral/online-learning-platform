import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InstructorStats } from '../models/models';

export interface AnalyticsDto {
  totalStudents: number;
  totalInstructors: number;
  totalCourses: number;
  totalEnrollments: number;
  completedCourses: number;
  categoryStats: CategoryStatDto[];
  popularCourses: PopularCourseDto[];
}

export interface CategoryStatDto {
  categoryName: string;
  courseCount: number;
  enrollmentCount: number;
}

export interface PopularCourseDto {
  courseId: number;
  courseTitle: string;
  enrollmentCount: number;
  averageCompletion: number;
}

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {

  private apiUrl = 'api/analytics';

  constructor(private http: HttpClient) { }

  getAnalytics(): Observable<AnalyticsDto> {
    return this.http.get<AnalyticsDto>(this.apiUrl);
  }

  getInstructorStats(): Observable<InstructorStats> {
    return this.http.get<InstructorStats>(`${this.apiUrl}/instructor`);
  }
}