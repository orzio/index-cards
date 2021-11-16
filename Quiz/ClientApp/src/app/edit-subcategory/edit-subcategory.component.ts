import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { CategoryService } from '../services/category-service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-subcategory',
  templateUrl: './edit-subcategory.component.html',
  styleUrls: ['./edit-subcategory.component.css']
})
export class EditSubcategoryComponent implements OnInit {

  constructor(private categoryService: CategoryService, private location: Location, private route: ActivatedRoute) { }

  categoryId: number;

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.categoryId = Number(params.get('categoryId'));
    })
  }

  categoryForm = new FormGroup({
    categoryName: new FormControl()
  })

  onSubmit() {
    let categoryName = this.categoryForm.value.categoryName;
    this.categoryService.addSubCategory(categoryName, this.categoryId).subscribe(() => this.goBack());
  }

  goBack() {
    this.location.back();
  }
}
