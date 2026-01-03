import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Quiz, QuizResult, QuizSubmission } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private apiUrl = `${environment.apiUrl}/quizzes`;

  constructor(private http: HttpClient) {}

  getQuiz(id: number): Observable<Quiz> {
    return this.http.get<Quiz>(`${this.apiUrl}/${id}`);
  }

  getCourseQuizzes(courseId: number): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${this.apiUrl}/course/${courseId}`);
  }

  submitQuiz(submission: QuizSubmission): Observable<QuizResult> {
    return this.http.post<QuizResult>(`${this.apiUrl}/submit`, submission);
  }

  getMyResults(quizId: number): Observable<QuizResult[]> {
    return this.http.get<QuizResult[]>(`${this.apiUrl}/${quizId}/my-results`);
  }

  createQuiz(quiz: any): Observable<Quiz> {
    return this.http.post<Quiz>(this.apiUrl, quiz);
  }

  updateQuiz(id: number, quiz: any): Observable<Quiz> {
    return this.http.put<Quiz>(`${this.apiUrl}/${id}`, quiz);
  }

  deleteQuiz(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
