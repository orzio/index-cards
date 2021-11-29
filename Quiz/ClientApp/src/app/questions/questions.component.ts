import { OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { Observable } from 'rxjs';
import { catchError, map, mergeMap, switchMap } from 'rxjs/operators';
import { QuestionService } from '../services/question-service';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit, OnDestroy {


  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  questions$: Observable<QuestionWithAnswer[]>;
  categoryId: number;
  private subscriptions = new Subscription;

  ngOnInit() {
    let sub = this.route.params.subscribe(
      (params) => {
        this.categoryId = (Number(params['categoryId']));
        this.questions$ = this.questionService.GetQuestionsForCategoryId(this.categoryId);
      }, error => {
        this.errorMessageSubject.next(error)
      });
    this.subscriptions.add(sub);

    let subQuestion = this.questionService.refreshQuestion.subscribe(() => {
      this.questions$ = this.questionService.GetQuestionsForCategoryId(this.categoryId);
    }, error => {
      this.errorMessageSubject.next(error)
    });
    this.subscriptions.add(subQuestion);
  }

  add() {
    this.router.navigate([`add-question`], { relativeTo: this.route })
  }
  constructor(private questionService: QuestionService, private route: ActivatedRoute, private router: Router) { 
    //var result = this.route.params.pipe(
    //  switchMap(params => this.questionService.GetQuestionsForCategoryId(Number(params.get('categoryId')))
    //    .pipe(map(questions =>
    //      questions.map(question => {
    //        return this.questionService.getAnswerByQuestionId(question.id).pipe(map(answer => {
    //          return {
    //            questionContent = question.content,
    //            questionId = question.id,
    //            answerContent = answer.Content,
    //            answerId = answer.id
    //          } as QuestionWithAnswer
    //        }
    //        ))
    //      })))))
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
    }
}
