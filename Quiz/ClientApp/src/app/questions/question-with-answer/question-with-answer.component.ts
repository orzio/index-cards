import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { QuestionService } from '../../services/question-service';

@Component({
  selector: 'app-question-with-answer',
  templateUrl: './question-with-answer.component.html',
  styleUrls: ['./question-with-answer.component.css']
})
export class QuestionWithAnswerComponent implements OnInit, OnDestroy {

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();


  private subscriptions = new Subscription();
  constructor(private questionService: QuestionService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
  }

  @Input() questionWithAnswer: QuestionWithAnswer;

  edit() {
    this.router.navigate([`edit-question/${this.questionWithAnswer.question.id}`], { relativeTo: this.route })
    console.log("add works");
  }

  remove() {
    let sub = this.questionService.removeQuestion(this.questionWithAnswer).subscribe(
      () => this.questionService.refreshQuestion.next(true),
      error => {
        this.errorMessageSubject.next(error)
      });
    this.subscriptions.add(sub);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

}
