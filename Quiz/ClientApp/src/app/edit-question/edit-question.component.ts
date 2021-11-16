import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../services/category-service';
import { QuestionService } from '../services/question-service';
import { Location } from '@angular/common';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'edit-question-component',
  templateUrl: './edit-question.component.html'
})


export class EditComponenet implements OnInit {

  questionId: number;
  categoryId: number;
  isEditMode: boolean;
  currentQuestion: QuestionWithAnswer;
  ngOnInit() {
    this.route.params.subscribe(
      (params) => {
        this.questionId = (Number(params['questionId']));
      });
    this.route.params.subscribe(
      (params) => {
        this.categoryId = (Number(params['categoryId']));
      });
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


  //  if(this.isEditMode) {
  //    let categoryName = this.categoryForm.value.categoryName;
  //    this.currentCategory.name = categoryName;
  //    this.categoryService.updateCategory(this.currentCategory).subscribe();
  //  }
  //    else {
  //  let categoryName = this.categoryForm.value.categoryName;
  //  this.categoryService.addCategory(categoryName).subscribe();
  //}


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
      this.questionService.updateQuestion(updatedQuestion, this.questionId).subscribe(() =>
        this.goBack());
    } else {
      this.questionService.createQuestion(question).subscribe(() =>
        this.goBack());

      
    }

  }

  InitializeForm() {
    if (this.questionId) {
      this.isEditMode = true;
      this.questionService.GetQuestionWithAnswerById(this.questionId).subscribe((questionWithAnswer: QuestionWithAnswer) => {
        this.currentQuestion = questionWithAnswer;
        this.questionForm.patchValue({
          questionContent: questionWithAnswer.question.content,
          questionAnswer: questionWithAnswer.answer.content
        })
      });
    }
  }


  goBack() {
    this.location.back();
  }
}


