import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CategoriesComponent } from './categories/categories.component';

import { DialogContentExampleDialog, HomeComponent } from './home/home.component';
import { EditComponenet } from './edit-question/edit-question.component';
import { QuizHomeComponent } from './quiz-home/quiz-home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatInputModule } from '@angular/material';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material';
import { NotFoundComponent } from './not-found/not-found.component';
import { QuizQuestionComponent } from './quiz-home/quiz-question/quiz-question.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { CategoryComponent } from './category/category.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { SubcategoriesComponent } from './subcategories/subcategories.component';
import { EditSubcategoryComponent } from './edit-subcategory/edit-subcategory.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { QuestionsComponent } from './questions/questions.component';
import { MatListModule } from '@angular/material/list';
import { QuestionWithAnswerComponent } from './questions/question-with-answer/question-with-answer.component';
import { SigninComponent } from './signin/signin.component';
import { SignupComponent } from './signup/signup.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthInterceptor } from './services/auth-interceptor';
import { StartComponent } from './start/start.component';
import { AuthGuardGuard } from './auth-guard.guard';


const materialModules = [
  MatSliderModule,
  MatSidenavModule,
  MatSelectModule,
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatTreeModule,
  MatIconModule,
  MatDialogModule,
  MatProgressSpinnerModule,
  MatCardModule,
  MatExpansionModule,
  MatGridListModule,
  MatListModule

]
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    EditComponenet,
    QuizHomeComponent,
    QuizQuestionComponent,
    NotFoundComponent,
    CategoriesComponent,
    CategoryComponent,
    EditCategoryComponent,
    SubcategoriesComponent,
    EditSubcategoryComponent,
    DialogContentExampleDialog,
    QuestionWithAnswerComponent,
    QuestionsComponent,
    SigninComponent,
    SignupComponent,
    StartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
   
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: StartComponent, pathMatch: 'full' },
      { path: 'signin', component: SigninComponent, pathMatch: 'full' },
      { path: 'signup', component: SignupComponent, pathMatch: 'full' },

      { path: 'show-categories/:categoryId/add-question', component: EditComponenet },
      { path: 'show-categories/:categoryId/edit-question/:questionId', component: EditComponenet, canActivate: [AuthGuardGuard] },
      { path: 'show-categories', component: CategoriesComponent, canActivate: [AuthGuardGuard]},
      { path: 'show-categories/add', component: EditCategoryComponent, canActivate: [AuthGuardGuard] },
      { path: 'show-categories/:categoryId', component: EditCategoryComponent, canActivate: [AuthGuardGuard] },
      { path: 'show-categories/:categoryId/add', component: EditSubcategoryComponent, canActivate: [AuthGuardGuard] },
      { path: 'quiz/category/:categoryId', component: QuizHomeComponent, pathMatch: 'full', canActivate: [AuthGuardGuard] },
      { path: 'quiz/:quizid/question/:questionId', component: QuizHomeComponent, pathMatch: 'full', canActivate: [AuthGuardGuard] },
      { path: 'home', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuardGuard] },
      //{ path: '**', component: NotFoundComponent, pathMatch: 'full' },
    ]),
    BrowserAnimationsModule,
    [...materialModules]
  ],
  entryComponents: [
    DialogContentExampleDialog
  ],

  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: false } },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
