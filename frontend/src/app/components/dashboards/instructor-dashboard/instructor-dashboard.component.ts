import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CourseService } from '../../../services/course.service';
import { AnalyticsService } from '../../../services/analytics.service';
import { Course, InstructorStats } from '../../../models/models';

@Component({
  selector: 'app-instructor-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './instructor-dashboard.component.html',
  styleUrls: ['./instructor-dashboard.component.css']
})
export class InstructorDashboardComponent implements OnInit {
  stats?: InstructorStats;
  courses: Course[] = [];
  loading = true;

  constructor(
    private courseService: CourseService,
    private analyticsService: AnalyticsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.courseService.getInstructorCourses().subscribe({
      next: (courses) => {
        this.courses = courses;
        this.loading = false;
      }
    });

    this.analyticsService.getInstructorStats().subscribe({
      next: (stats: InstructorStats) => {
        this.stats = stats;
      }
    });
  }

  createCourse(): void {
    this.router.navigate(['/instructor/courses/new']);
  }

  editCourse(courseId: number): void {
    this.router.navigate(['/instructor/courses', courseId, 'edit']);
  }

  getCourseImage(course: Course): string {
    if (course.thumbnailUrl) {
      return course.thumbnailUrl;
    }
    // Return placeholder images
    const placeholders = [
      'https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1501504905252-473c47e087f8?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1488190211105-8b0e65b80b4e?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1546410531-bb4caa6b424d?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1522202176988-66273c2fd55f?w=600&h=400&fit=crop'
    ];
    const index = Math.abs(course.id) % placeholders.length;
    return placeholders[index];
  }
}
