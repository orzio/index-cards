import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from '../../main';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, Subject, Subscribable, Subscription } from 'rxjs';
import { Observable } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { QuestionService } from '../services/question-service';
import { QuizService } from '../services/quiz-service';
import { OnDestroy } from '@angular/core';
import { log } from 'util';
import { Location } from '@angular/common';

@Component({
  selector: 'quiz-home',
  templateUrl: './quiz-home.component.html',
  styleUrls: ['./quiz-home.component.css']
})
export class QuizHomeComponent implements OnInit, OnDestroy {
  public forecasts: Question[] = [];

  public question$: Observable<Question>;
  public quizId: number;

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  private subs = new Subscription;
  constructor(private quizService: QuizService, private location: Location,
    private activatedRoute: ActivatedRoute, private router:Router) {}

  ngOnInit(): void {
    let sub = this.activatedRoute.paramMap.pipe(
      switchMap(params => this.quizService.createQuiz(Number(params.get('categoryId')))
      ))
      .subscribe(quiz => {
        this.quizId = quiz.id;
        this.nextQuestion();
      },
        error => {
          this.errorMessageSubject.next(error)
        });
    this.subs.add(sub);

  }

  nextQuestion() {
    this.quizService.NextQuestionClicked.next(this.quizId);
  }

  checkAnswer() {
    this.quizService.checkAnswerClicked.next(this.quizId);
  }

  showAnswer() {
    console.log("SAC");
    this.quizService.showAnswerClicked.next(true);
  }

  ngOnDestroy(): void {
    console.log("before destroy the quiz id is: " + this.quizId);
    console.log("I DESTROY component");
    this.subs.unsubscribe();
  }

  goBack() {
    this.location.back();
  }
}

