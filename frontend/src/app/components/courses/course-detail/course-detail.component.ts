import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../../../services/course.service';
import { EnrollmentService } from '../../../services/enrollment.service';
import { AuthService } from '../../../services/auth.service';
import { Course } from '../../../models/models';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course?: Course;
  loading = true;
  enrolling = false;
  isEnrolled = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private courseService: CourseService,
    private enrollmentService: EnrollmentService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.courseService.getCourse(+id).subscribe({
      next: (course) => {
        this.course = course;
        this.loading = false;
        this.checkEnrollment();
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  checkEnrollment(): void {
    if (this.authService.isLoggedIn && this.course) {
      this.enrollmentService.getMyEnrollments().subscribe({
        next: (enrollments) => {
          this.isEnrolled = enrollments.some(e => e.courseId === this.course?.id);
        }
      });
    }
  }

  enroll(): void {
    if (!this.authService.isLoggedIn) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.course) return;

    this.enrolling = true;
    this.enrollmentService.enroll(this.course.id).subscribe({
      next: () => {
        this.isEnrolled = true;
        this.enrolling = false;
        this.router.navigate(['/student/dashboard']);
      },
      error: () => {
        this.enrolling = false;
      }
    });
  }

  getTotalDuration(): string {
    if (!this.course) return '0h 0m';
    const minutes = Math.floor(this.course.duration / 60);
    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;
    return `${hours}h ${remainingMinutes}m`;
  }

  getDuration(seconds: number): number {
    return Math.floor(seconds / 60);
  }
}
