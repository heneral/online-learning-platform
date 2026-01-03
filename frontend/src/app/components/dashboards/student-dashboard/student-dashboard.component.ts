import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EnrollmentService } from '../../../services/enrollment.service';
import { Enrollment } from '../../../models/models';

@Component({
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  enrollments: Enrollment[] = [];
  loading = true;

  constructor(
    private enrollmentService: EnrollmentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEnrollments();
  }

  loadEnrollments(): void {
    this.enrollmentService.getMyEnrollments().subscribe({
      next: (enrollments) => {
        this.enrollments = enrollments;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  continueLearning(enrollment: Enrollment): void {
    const firstLesson = enrollment.course.lessons?.[0];
    if (firstLesson) {
      this.router.navigate(['/learn', enrollment.id, 'lesson', firstLesson.id]);
    }
  }

  getCompletedCount(): number {
    return this.enrollments.filter(e => e.progressPercentage === 100).length;
  }

  getInProgressCount(): number {
    return this.enrollments.filter(e => e.progressPercentage > 0 && e.progressPercentage < 100).length;
  }

  getCourseImage(course: any): string {
    if (course.thumbnailUrl) {
      return course.thumbnailUrl;
    }
    // Return placeholder images based on course level or title
    const placeholders = [
      'https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1501504905252-473c47e087f8?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1488190211105-8b0e65b80b4e?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1546410531-bb4caa6b424d?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1522202176988-66273c2fd55f?w=600&h=400&fit=crop'
    ];
    const index = Math.abs(course.id.split('-')[0].charCodeAt(0)) % placeholders.length;
    return placeholders[index];
  }

  getCourseCategory(level: string): string {
    const categories: any = {
      'Beginner': '🎯 Beginner',
      'Intermediate': '📈 Intermediate',
      'Advanced': '🚀 Advanced'
    };
    return categories[level] || '📚 ' + level;
  }
}
