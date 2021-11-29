import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SUPER_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { combineLatest, Observable, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map } from 'rxjs/operators';
import { QuizErrorHandlerService } from './error-handlers/quiz-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  constructor(private httpClient: HttpClient, private activatedRoute: ActivatedRoute, private errorHandlerService: QuizErrorHandlerService) { }
  private quizUrl = 'quiz';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  public createQuiz(categoryId: number): Observable<Quiz> {
    return this.httpClient.post<Quiz>(`${this.quizUrl}/category`, { categoryId }, this.httpOptions).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }

  public question$: Observable<Question>;

  public getQuestionWithStatusForQuiz(quizId: number): Observable<QuestionWithStatus> {
    console.log("quiz started with id: "+quizId)
    return this.httpClient.get<QuestionWithStatus>(`${this.quizUrl}/question/${quizId}`).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }


  public checkQuizStatus(quizId: number): Observable<QuizStatusId> {
    console.log("servis" + quizId);
    return this.httpClient.get<QuizStatusId>(`${this.quizUrl}/${quizId}/status`).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }

  public NextQuestionClicked = new Subject<number>();
  public quizNumberChangedAction$ = this.NextQuestionClicked.asObservable();

  public checkAnswerClicked = new Subject<number>();
  public checkAnswerAction$ = this.checkAnswerClicked.asObservable();

  public showAnswerClicked = new Subject<boolean>();
  public showAnswerAction$ = this.showAnswerClicked.asObservable();

  checkAnswer(currentQuestionId: number, currentQuizId: number, answer: string): Observable<AnswerResult> {
    return this.httpClient.post<AnswerResult>(`${this.quizUrl}/checkAnswer`, {
      CurrentQuestionId: currentQuestionId,
      CurrentQuizId:currentQuizId,
      Answer: answer
    }, this.httpOptions).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }
}
