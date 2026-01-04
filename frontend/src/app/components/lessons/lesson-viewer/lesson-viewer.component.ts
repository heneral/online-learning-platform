import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { EnrollmentService } from '../../../services/enrollment.service';
import { LessonProgress, Lesson } from '../../../models/models';

@Component({
  selector: 'app-lesson-viewer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lesson-viewer.component.html',
  styleUrls: ['./lesson-viewer.component.css']
})
export class LessonViewerComponent implements OnInit {
  enrollmentId!: number;
  lessonId!: number;
  lesson?: any;
  progress?: LessonProgress;

  constructor(
    private route: ActivatedRoute,
    private enrollmentService: EnrollmentService
  ) {}

  ngOnInit(): void {
    this.enrollmentId = +this.route.snapshot.params['enrollmentId'];
    this.lessonId = +this.route.snapshot.params['lessonId'];
    this.loadLesson();
  }

  loadLesson(): void {
    // Load lesson and progress
    this.enrollmentService.getEnrollment(this.enrollmentId).subscribe({
      next: (enrollment) => {
        if (enrollment.course?.lessons) {
          this.lesson = enrollment.course.lessons.find((l: Lesson) => l.id === this.lessonId);
        }
      }
    });
  }

  markComplete(): void {
    if (this.lesson) {
      const progress: LessonProgress = {
        id: 0, // Will be set by the backend
        lessonId: this.lessonId,
        enrollmentId: this.enrollmentId,
        isCompleted: true,
        watchTimeInSeconds: 0, // You might want to track actual watch time
        lastWatchedAt: new Date().toISOString(),
        completedAt: new Date().toISOString()
      };
      this.enrollmentService.updateProgress(this.enrollmentId, [progress]).subscribe({
        next: () => {
          alert('Lesson marked as complete!');
        }
      });
    }
  }
}
