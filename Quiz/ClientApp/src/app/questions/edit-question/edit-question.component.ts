import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../services/category-service';
import { QuestionService } from '../../services/question-service';
import { Location } from '@angular/common';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'edit-question-component',
  templateUrl: './edit-question.component.html'
})


export class EditComponenet implements OnInit, OnDestroy {

  questionId: number;
  categoryId: number;
  isEditMode: boolean;
  currentQuestion: QuestionWithAnswer;
  private subscriptions = new Subscription();

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  ngOnInit() {
    let subQuestion = this.route.params.subscribe(
      (params) => {
        this.questionId = (Number(params['questionId']));
      },
      error => {
        this.errorMessageSubject.next(error)
      });
    let subRoute = this.route.params.subscribe(
      (params) => {
        this.categoryId = (Number(params['categoryId']));
      },
      error => {
        this.errorMessageSubject.next(error)
      });
    this.subscriptions.add(subQuestion)
      .add(subRoute);
    this.InitializeForm();
  }

  constructor(private categoryService: CategoryService,
    private questionService:
      QuestionService, private location: Location,
    private route: ActivatedRoute) { }

  selectedCategoryName = new FormControl();
  selected: Category;
  questionForm = new FormGroup({
    questionContent: new FormControl('', [Validators.required]),
    questionAnswer: new FormControl('', [Validators.required]),
  });

  onSubmit() {
    console.warn(this.questionForm.value);
    let question = {
      categoryId: (this.categoryId),
      content: this.questionForm.value.questionContent,
      answer: this.questionForm.value.questionAnswer,
    } as Question;

    if (this.isEditMode) {
      let updatedQuestion = {
        QuestionContent: this.questionForm.value.questionContent,
        AnswerContent: this.questionForm.value.questionAnswer
      } as UpdatedQuestionWithAnswer
      let sub = this.questionService.updateQuestion(updatedQuestion, this.questionId).subscribe(() =>
        this.goBack(),
        error => {
          this.errorMessageSubject.next(error)
        });
      this.subscriptions.add(sub);
    } else {
      let sub = this.questionService.createQuestion(question).subscribe(() =>
        this.goBack(),
        error => {
          this.errorMessageSubject.next(error)
        });
      this.subscriptions.add(sub);
    }

  }

  InitializeForm() {
    if (this.questionId) {
      this.isEditMode = true;
      let sub = this.questionService.GetQuestionWithAnswerById(this.questionId).subscribe((questionWithAnswer: QuestionWithAnswer) => {
        this.currentQuestion = questionWithAnswer;
        this.questionForm.patchValue({
          questionContent: questionWithAnswer.question.content,
          questionAnswer: questionWithAnswer.answer.content
        })
      },
        error => {
          this.errorMessageSubject.next(error)
        });
      this.subscriptions.add(sub);
    }
  }

  goBack() {
    this.location.back();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}


