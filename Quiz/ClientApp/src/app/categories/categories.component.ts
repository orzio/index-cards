import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../services/category-service';
import { QuestionService } from '../services/question-service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html'
})
export class CategoriesComponent {

  constructor(private categoryService: CategoryService, private route: ActivatedRoute, private router:Router) { }

  categories$ = this.categoryService.categories$;

  goToCategory(category: Category) {
    this.router.navigate([`${category.id}`], { relativeTo: this.route })
  }

  addCategory() {
    this.router.navigate([`add`], { relativeTo: this.route })
  }
}
