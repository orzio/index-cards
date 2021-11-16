import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { combineLatest, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map } from 'rxjs/operators';

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
      catchError(this.handleError)
    );

  categories$ = this.httpClient.get<Category[]>(this.categoriesUrl)
    .pipe(
      tap(data => console.log('categories', JSON.stringify(data))),
      catchError(this.handleError)
    );

  selectedCategorySubject = new Subject<number>();
  selectedCategoryChanged$ = this.selectedCategorySubject.asObservable();

  public getSubcategories(id: number) {
    console.log("subcategories" + id);
    return this.httpClient.get<Category[]>(`${this.categoriesUrl}/${id}/${this.subcategoriesUrl}`);
  }

  public getCategoryById(id: number) {
    return this.httpClient.get<Category>(`${this.categoriesUrl}/${id}`).pipe(tap(data => console.log('categories', JSON.stringify(data))));
  }

  public removeCategory(id: number) {
    return this.httpClient.delete(`${this.categoriesUrl}/${id}`);
  }
  public refreshCategories= new Subject<boolean>();

  //categoriesWithSubCategories$ = combineLatest([
  //  this.subCategories$,
  //  this.categories$]).pipe(
  //    map(([subcategories, categories]) => ({
  //      categories.map(category => ...category,
  //        subCategories: categories)
  //    }) as Category));


  constructor(private httpClient: HttpClient) { }

  private handleError(err: any) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }

  addCategory(name: string) {
    console.log("add category: " + name);
    return this.httpClient.post<Category>(this.categoriesUrl, { name }, this.httpOptions);
  }


  addSubCategory(name: string, parentId: number) {
    console.log("add category: " + name + "parent id: " + parentId);
    let subactagory = { name, parentId } as CreateSubcategory;
    return this.httpClient.post<Category>(`${this.categoriesUrl}/subcategories`, { name, parentId } ,this.httpOptions);
  }



  updateCategory(category: Category) {
    return this.httpClient.put<Category>(`${this.categoriesUrl}/${category.id}`, category, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}


//https://angular.io/guide/http
