import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, mergeMap, switchMap } from 'rxjs/operators';
import { QuestionService } from '../services/question-service';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  questions$: Observable<QuestionWithAnswer[]>;
  categoryId: number;
  ngOnInit() {
    this.route.params.subscribe(
      (params) => {
        this.categoryId = (Number(params['categoryId']));
        this.questions$ = this.questionService.GetQuestionsForCategoryId(this.categoryId);
      });

    this.questionService.refreshQuestion.subscribe(() => {
      console.log("123123213odsiwezam")
      this.questions$ = this.questionService.GetQuestionsForCategoryId(this.categoryId);
    });
  }

  add() {
    this.router.navigate([`add-question`], { relativeTo: this.route })
    console.log("add works");
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
}
