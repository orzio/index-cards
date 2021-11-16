import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SUPER_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { combineLatest, Observable, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  constructor(private httpClient: HttpClient, private activatedRoute: ActivatedRoute) { }
  private quizUrl = 'quiz';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  public createQuiz(categoryId: number): Observable<Quiz> {
    return this.httpClient.post<Quiz>(`${this.quizUrl}/category`, { categoryId }, this.httpOptions);
  }
  public question$: Observable<Question>;

  public getQuestionWithStatusForQuiz(quizId: number): Observable<QuestionWithStatus> {
    console.log("quiz started with id: "+quizId)
    return this.httpClient.get<QuestionWithStatus>(`${this.quizUrl}/question/${quizId}`);
  }


  public checkQuizStatus(quizId: number): Observable<QuizStatusId> {
    console.log("servis" + quizId);
    return this.httpClient.get<QuizStatusId>(`${this.quizUrl}/${quizId}/status`);
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
    }, this.httpOptions)
  }

  private handleError(err: any) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
