import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { CategoryService } from '../../../services/category-service';
import { Location } from '@angular/common';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { OnDestroy } from '@angular/core';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-edit-subcategory',
  templateUrl: './edit-subcategory.component.html',
  styleUrls: ['./edit-subcategory.component.css']
})
export class EditSubcategoryComponent implements OnInit, OnDestroy {

  constructor(private categoryService: CategoryService, private location: Location, private route: ActivatedRoute) { }

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();
  categoryId: number;
  private subscriptions = new Subscription();


  ngOnInit() {
    let sub = this.route.paramMap.subscribe((params: ParamMap) => {
      this.categoryId = Number(params.get('categoryId'));
    }, error => {
      this.errorMessageSubject.next(error)
    })
    this.subscriptions.add(sub);
  }

  categoryForm = new FormGroup({
    categoryName: new FormControl()
  })

  onSubmit() {
    let categoryName = this.categoryForm.value.categoryName;
    this.categoryService.addSubCategory(categoryName, this.categoryId).subscribe(() => this.goBack(),
      error => {
        this.errorMessageSubject.next(error)
      });
  }

  goBack() {
    this.location.back();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

}
