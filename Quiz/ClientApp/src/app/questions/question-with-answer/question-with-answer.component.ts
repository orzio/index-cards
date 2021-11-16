import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionService } from '../../services/question-service';

@Component({
  selector: 'app-question-with-answer',
  templateUrl: './question-with-answer.component.html',
  styleUrls: ['./question-with-answer.component.css']
})
export class QuestionWithAnswerComponent implements OnInit {

  constructor(private questionService: QuestionService, private route:ActivatedRoute, private router:Router) { }

  ngOnInit() {
  }

  @Input() questionWithAnswer: QuestionWithAnswer;

  edit() {
    this.router.navigate([`edit-question/${this.questionWithAnswer.question.id}`], { relativeTo: this.route })
    console.log("add works");
  }

  remove() {
    this.questionService.removeQuestion(this.questionWithAnswer).subscribe(
      () => this.questionService.refreshQuestion.next(true));
  }
}
