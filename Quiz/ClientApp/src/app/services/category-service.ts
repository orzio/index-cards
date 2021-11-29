import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { combineLatest, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map } from 'rxjs/operators';
import { QuizErrorHandlerService } from './error-handlers/quiz-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  private categoriesUrl = 'categories';
  private subcategoriesUrl = 'subcategories';

  quizCategories$ = this.httpClient.get<Category[]>(`${this.categoriesUrl}/quizes`)
    .pipe(
      tap(data => console.log('quiz-categories', JSON.stringify(data))),
      catchError(this.errorHandlerService.handleError)
    );

  categories$ = this.httpClient.get<Category[]>(this.categoriesUrl)
    .pipe(
      catchError(this.errorHandlerService.handleError)
    );

  selectedCategorySubject = new Subject<number>();
  selectedCategoryChanged$ = this.selectedCategorySubject.asObservable();

  public getSubcategories(id: number) {
    console.log("subcategories" + id);
    return this.httpClient.get<Category[]>(`${this.categoriesUrl}/${id}/${this.subcategoriesUrl}`).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }

  public getCategoryById(id: number) {
    return this.httpClient.get<Category>(`${this.categoriesUrl}/${id}`).pipe(tap(data => console.log('categories', JSON.stringify(data))),
      catchError(this.errorHandlerService.handleError)
    );
  }

  public removeCategory(id: number) {
    return this.httpClient.delete(`${this.categoriesUrl}/${id}`).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }
  public refreshCategories = new Subject<boolean>();

  //categoriesWithSubCategories$ = combineLatest([
  //  this.subCategories$,
  //  this.categories$]).pipe(
  //    map(([subcategories, categories]) => ({
  //      categories.map(category => ...category,
  //        subCategories: categories)
  //    }) as Category));


  constructor(private httpClient: HttpClient, private errorHandlerService:QuizErrorHandlerService) { }


  addCategory(name: string) {
    console.log("add category: " + name);
    return this.httpClient.post<Category>(this.categoriesUrl, { name }, this.httpOptions)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }


  addSubCategory(name: string, parentId: number) {
    console.log("add category: " + name + "parent id: " + parentId);
    let subactagory = { name, parentId } as CreateSubcategory;
    return this.httpClient.post<Category>(`${this.categoriesUrl}/subcategories`, { name, parentId }, this.httpOptions)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }



  updateCategory(category: Category) {
    return this.httpClient.put<Category>(`${this.categoriesUrl}/${category.id}`, category, this.httpOptions)
      .pipe(
        catchError(this.errorHandlerService.handleError)
      );
  }
}


//https://angular.io/guide/http
