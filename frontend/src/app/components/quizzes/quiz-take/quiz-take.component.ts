import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../../../services/quiz.service';
import { Quiz, QuizSubmission } from '../../../models/models';

@Component({
  selector: 'app-quiz-take',
  templateUrl: './quiz-take.component.html',
  styleUrls: ['./quiz-take.component.css']
})
export class QuizTakeComponent implements OnInit {
  quiz?: Quiz;
  answers: { [questionId: number]: number } = {};
  submitting = false;
  startTime: Date = new Date();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private quizService: QuizService
  ) {}

  ngOnInit(): void {
    const quizId = +this.route.snapshot.params['quizId'];
    this.quizService.getQuiz(quizId).subscribe({
      next: (quiz) => {
        this.quiz = quiz;
        this.startTime = new Date();
      }
    });
  }

  selectAnswer(questionId: number, answerId: number): void {
    this.answers[questionId] = answerId;
  }

  submitQuiz(): void {
    if (!this.quiz) return;

    const submission: QuizSubmission = {
      quizId: this.quiz.id,
      answers: Object.entries(this.answers).map(([questionId, answerId]) => ({
        questionId: +questionId,
        selectedAnswerId: answerId
      })),
      timeSpentInSeconds: Math.floor((new Date().getTime() - this.startTime.getTime()) / 1000)
    };

    this.submitting = true;
    this.quizService.submitQuiz(submission).subscribe({
      next: (result) => {
        alert(`Quiz completed! Score: ${result.score}/${result.totalPoints}. ${result.passed ? 'Passed!' : 'Try again.'}`);
        this.router.navigate(['/student/dashboard']);
      },
      error: () => {
        this.submitting = false;
      }
    });
  }

  isAnswered(questionId: number): boolean {
    return !!this.answers[questionId];
  }

  canSubmit(): boolean {
    if (!this.quiz) return false;
    return this.quiz.questions.every(q => this.isAnswered(q.id));
  }
}
