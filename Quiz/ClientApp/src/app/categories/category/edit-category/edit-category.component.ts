import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CategoryService } from '../../../services/category-service';
import { Location } from '@angular/common';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { OnDestroy } from '@angular/core';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css'],
})
export class EditCategoryComponent implements OnInit, OnDestroy {

  constructor(private categoryService: CategoryService, private location: Location, private route: ActivatedRoute) { }

  lastValueInUrl: string;
  isEditMode: boolean;
  currentCategory: Category;
  categoryId: number;
  private subscriptions = new Subscription();
  isSubcategory = true;

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  ngOnInit() {
    let sub = this.route.paramMap.subscribe((params: ParamMap) => {
      this.categoryId = Number(params.get('categoryId'));
      this.InitializeForm();
    },
      error =>
        this.errorMessageSubject.next(error)
    );
    this.subscriptions.add(sub);
  }

  categoryForm = new FormGroup({
    categoryName: new FormControl()
  })

  onSubmit() {
    if (this.isEditMode) {
      let categoryName = this.categoryForm.value.categoryName;
      this.currentCategory.name = categoryName;
      let sub = this.categoryService.updateCategory(this.currentCategory).subscribe(() => { }, error =>
        this.errorMessageSubject.next(error)
      );
      this.subscriptions.add(sub);
    }
    else {
      let categoryName = this.categoryForm.value.categoryName;
      let sub = this.categoryService.addCategory(categoryName).subscribe(() => this.goBack(),
        error =>
          this.errorMessageSubject.next(error)
      );
      this.subscriptions.add(sub);
    }
  }

  goBack() {
    this.location.back();
  }

  InitializeForm() {
    if (this.categoryId) {
      this.isEditMode = true;
        let sub = this.categoryService.getCategoryById(this.categoryId).subscribe((category: Category) => {
          this.currentCategory = category;
          this.isSubcategory = !!category.parentCategoryId;
          this.categoryForm.patchValue({
            categoryName: category.name
          })
        }, error =>
          this.errorMessageSubject.next(error)
        );

        this.subscriptions.add(sub);
    }
  }
  removeCategory() {
    let sub = this.categoryService.removeCategory(this.currentCategory.id).subscribe(() => {
      this.goBack();
    },
      error =>
        this.errorMessageSubject.next(error)
    );
    this.subscriptions.add(sub);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
