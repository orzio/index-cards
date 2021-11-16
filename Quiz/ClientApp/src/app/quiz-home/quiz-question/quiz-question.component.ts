import { OnInit } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { combineLatest, merge, Observable, Subscription } from 'rxjs';
import { map, mergeMap, switchMap, tap } from 'rxjs/operators';
import { log } from 'util';
import { QuestionService } from '../../services/question-service';
import { QuizService } from '../../services/quiz-service';

@Component({
  selector: 'app-quiz-question',
  templateUrl: './quiz-question.component.html',
  styleUrls: ['./quiz-question.component.css']
})

export class QuizQuestionComponent implements OnInit, OnDestroy {
  constructor(private quizService: QuizService, private activatedRoute: ActivatedRoute, private questionService: QuestionService) { }

  answer = new FormControl('');
  currentQuestion: QuestionDto;
  private currentQuestionId: number;
  private currentQuizId: number;
  answerResult: AnswerResult;
  correctAnswer: Answer;

  isActive = true;
  private subscription: Subscription;

  ngOnInit(): void {
    this.subscription=  this.quizService.checkAnswerAction$.pipe(
      switchMap(quizId => this.quizService.checkAnswer(
        this.currentQuestionId,
        this.currentQuizId,
        this.answer.value)))
      .subscribe((data: AnswerResult) => {
        this.answerResult = data;
      });

    this.quizService.showAnswerAction$.pipe(
      switchMap(_ => this.questionService.getAnswerByQuestionId(this.currentQuestionId))
    ).subscribe(answer => this.correctAnswer = answer);
  }

  status$ = this.quizService.quizNumberChangedAction$
    .pipe(
      tap(quizId => this.currentQuizId = quizId),
      switchMap(quizId =>
        this.quizService.checkQuizStatus(quizId))
    );


  combine$ = this.quizService.quizNumberChangedAction$.pipe(
    tap(quizId => this.currentQuizId = quizId),
    mergeMap(quizId => this.quizService.getQuestionWithStatusForQuiz(quizId))
  ).pipe(map((questionWithStatus: QuestionWithStatus) => {
    this.SetStatus(questionWithStatus.quizStatusDto.status);
    if (Number(status) == 2) {
      return;
    } else {
      console.log(JSON.stringify(questionWithStatus));
      this.currentQuestion = questionWithStatus.questionDto;
      this.currentQuestionId = +questionWithStatus.questionDto.id;
      this.answerResult = null;
      this.answer.setValue('');
      this.correctAnswer = null;
    }
  }));

  private SetStatus(status: number) {
    this.isActive = status != 2;
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}


/*
  ngOnInit(): void {
    this.subscription = this.quizService.quizNumberChangedAction$
      .pipe(
        tap(quizId => this.currentQuizId = quizId),
        switchMap(quizId =>
          this.quizService.getQuestionForQuiz(quizId))
      ).subscribe(question => {
        this.currentQuestion = question;
        this.currentQuestionId = +question.id;
        this.answerResult = null;
        this.answer.setValue('');
        this.correctAnswer = null;
      });

    this.quizService.checkAnswerAction$.pipe(
      switchMap(quizId => this.quizService.checkAnswer(this.currentQuestionId,
        this.currentQuizId,
        this.answer.value))).subscribe((data: AnswerResult) => {
          this.answerResult = data;
        });

   this.quizService.showAnswerAction$.pipe(
      switchMap(_ => this.questionService.getAnswerByQuestionId(this.currentQuestionId))
    ).subscribe(answer => this.correctAnswer = answer);
  }

*/
