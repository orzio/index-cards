import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionWithAnswerComponent } from './question-with-answer.component';

describe('QuestionWithAnswerComponent', () => {
  let component: QuestionWithAnswerComponent;
  let fixture: ComponentFixture<QuestionWithAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionWithAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionWithAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
