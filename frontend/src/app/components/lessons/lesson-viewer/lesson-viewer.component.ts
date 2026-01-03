import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EnrollmentService } from '../../../services/enrollment.service';
import { LessonProgress } from '../../../models/models';

@Component({
  selector: 'app-lesson-viewer',
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
        this.lesson = enrollment.course.lessons?.find(l => l.id === this.lessonId);
      }
    });
  }

  markComplete(): void {
    this.enrollmentService.completLesson(this.enrollmentId, this.lessonId).subscribe({
      next: () => {
        alert('Lesson marked as complete!');
      }
    });
  }
}
