import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { BehaviorSubject, combineLatest, merge, Observable, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map, switchMap, concatMap, scan } from 'rxjs/operators';
import { CategoriesComponent } from '../categories/categories.component';
import { QuizErrorHandlerService } from './error-handlers/quiz-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {


  constructor(private httpClient: HttpClient, private activatedRoute: ActivatedRoute, private errorHandlerService:QuizErrorHandlerService) { }
  private questionUrl = 'questions';
  private subcategoriesUrl = 'subcategories';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  updateQuestion(question: UpdatedQuestionWithAnswer, questionId: number) {
    return this.httpClient.put(`${this.questionUrl}/${questionId}`, question, this.httpOptions)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }
  public createQuestion(question: Question): Observable<Question> {
    console.log(`question${question.answer}`);

    return this.httpClient.post(this.questionUrl, question, this.httpOptions).pipe(
      tap((newQuestion: Question) => console.log(`NewQuestion ${newQuestion}`)),
      catchError(this.errorHandlerService.handleError));
  }

  public GetQuestionsForCategoryId(categoryId: number): Observable<QuestionWithAnswer[]> {
    console.log("categoryId" + categoryId);
    return this.httpClient.get<QuestionWithAnswer[]>(`/categories/${categoryId}/questions`).pipe(
      catchError(this.errorHandlerService.handleError)
    )
  }


  public GetQuestionWithAnswerById(questionId: number): Observable<QuestionWithAnswer> {
    console.log("categoryId" + questionId);
    return this.httpClient.get<QuestionWithAnswer>(`/questions/${questionId}`).pipe(
      catchError(this.errorHandlerService.handleError)
    )
  }

  public getAnswerByQuestionId(currentQuestionId: number): Observable<Answer> {
    console.log("getAnswer");
    return this.httpClient.get<Answer>(`${this.questionUrl}/answer/${currentQuestionId}`).pipe(
      catchError(this.errorHandlerService.handleError)
    )
  }

  public removeQuestion(questionWithAnswer: QuestionWithAnswer) {
    return this.httpClient.delete(`${this.questionUrl}/${questionWithAnswer.question.id}`).pipe(
      catchError(this.errorHandlerService.handleError)
    )
  }

  public refreshQuestion = new Subject<boolean>();

}
