import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { BehaviorSubject, combineLatest, merge, Observable, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map, switchMap, concatMap, scan } from 'rxjs/operators';
import { CategoriesComponent } from '../categories/categories.component';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {


  constructor(private httpClient: HttpClient, private activatedRoute: ActivatedRoute) { }
  private questionUrl = 'questions';
  private subcategoriesUrl = 'subcategories';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  updateQuestion(question: UpdatedQuestionWithAnswer, questionId: number) {
    return this.httpClient.put(`${this.questionUrl}/${questionId}`, question, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
  public createQuestion(question: Question): Observable<Question> {
    console.log(`question${question.answer}`);

    return this.httpClient.post(this.questionUrl, question, this.httpOptions).pipe(
      tap((newQuestion: Question) => console.log(`NewQuestion ${newQuestion}`)),
      catchError(this.handleError));
  }

  public GetQuestionsForCategoryId(categoryId: number): Observable<QuestionWithAnswer[]> {
    console.log("categoryId" + categoryId);
    return this.httpClient.get<QuestionWithAnswer[]>(`/categories/${categoryId}/questions`)
  }


  public GetQuestionWithAnswerById(questionId: number): Observable<QuestionWithAnswer> {
    console.log("categoryId" + questionId);
    return this.httpClient.get<QuestionWithAnswer>(`/questions/${questionId}`)
  }

  public getAnswerByQuestionId(currentQuestionId: number): Observable<Answer> {
    console.log("getAnswer");
    return this.httpClient.get<Answer>(`${this.questionUrl}/answer/${currentQuestionId}`)
  }

  public removeQuestion(questionWithAnswer: QuestionWithAnswer) {
    return this.httpClient.delete(`${this.questionUrl}/${questionWithAnswer.question.id}`);
  }

  public refreshQuestion = new Subject<boolean>();



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
