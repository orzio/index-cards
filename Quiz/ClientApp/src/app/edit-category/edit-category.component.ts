import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CategoryService } from '../services/category-service';
import { Location } from '@angular/common';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {

  constructor(private categoryService: CategoryService, private location: Location, private route: ActivatedRoute) { }
  lastValueInUrl: string;
  isEditMode: boolean;
  currentCategory: Category;
  categoryId: number;

  isSubcategory = true;

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.categoryId = Number(params.get('categoryId'));
      this.InitializeForm();
    })
  }

  categoryForm = new FormGroup({
    categoryName: new FormControl()
  })

  onSubmit() {
    if (this.isEditMode) {
      let categoryName = this.categoryForm.value.categoryName;
      this.currentCategory.name = categoryName;
      this.categoryService.updateCategory(this.currentCategory).subscribe();
    }
    else {
      let categoryName = this.categoryForm.value.categoryName;
      this.categoryService.addCategory(categoryName).subscribe(() => this.goBack());
    }
  }

  goBack() {
    this.location.back();
  }

  InitializeForm() {
    if (this.categoryId) {
      this.isEditMode = true;
      this.categoryService.getCategoryById(this.categoryId).subscribe((category: Category) => {
        this.currentCategory = category;
        this.isSubcategory = !!category.parentCategoryId;
        this.categoryForm.patchValue({
          categoryName: category.name
        })
      });
    }
  }
  removeCategory() {
    this.categoryService.removeCategory(this.currentCategory.id).subscribe(() => {
      this.goBack();
    });
  }
}
