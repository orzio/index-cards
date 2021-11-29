import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { CategoryService } from '../../services/category-service';

@Component({
  selector: 'app-subcategories',
  templateUrl: './subcategories.component.html',
  styleUrls: ['./subcategories.component.css']
})
export class SubcategoriesComponent implements OnInit {

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  constructor(private categoryService: CategoryService, private route: ActivatedRoute, private router: Router) { }

  categories$ = this.route.paramMap.pipe(
    switchMap(params => this.categoryService.getSubcategories(Number(params.get('categoryId')))
    )).pipe(
      catchError(err => {
        this.errorMessageSubject.next(err);
        return EMPTY;
      })
    );

  ngOnInit(): void {}

  goToCategory(category: Category) {
    this.router.navigate([`../${category.id}`], { relativeTo: this.route })
  }

  addCategory() {
    this.router.navigate([`add`], { relativeTo: this.route })
  }

}
