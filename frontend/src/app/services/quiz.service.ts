import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Quiz, QuizResult, QuizSubmission } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  private apiUrl = 'api/quizzes';

  constructor(private http: HttpClient) { }

  getQuizzes(courseId: number): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${this.apiUrl}?courseId=${courseId}`);
  }

  getQuiz(id: number): Observable<Quiz> {
    return this.http.get<Quiz>(`${this.apiUrl}/${id}`);
  }

  submitQuiz(submission: QuizSubmission): Observable<QuizResult> {
    return this.http.post<QuizResult>(`${this.apiUrl}/submit`, submission);
  }

  getResults(quizId: number): Observable<QuizResult[]> {
    return this.http.get<QuizResult[]>(`${this.apiUrl}/${quizId}/results`);
  }
}
