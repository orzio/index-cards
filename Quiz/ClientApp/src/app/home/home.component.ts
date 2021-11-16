import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { Inject, OnDestroy, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeNestedDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { CategoryService } from '../services/category-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  treeControl = new NestedTreeControl<Category>(node => node.subCategories);
  dataSource = new MatTreeNestedDataSource<Category>();

  private categories$ = this.categoryService.quizCategories$;
  private _subscription = new Subscription();

  constructor(private categoryService: CategoryService, public dialog: MatDialog) { }
  ngOnInit(): void {
    this._subscription = this.categories$.subscribe((data: Category[]) => {
      this.dataSource.data = data
    });
  }

  activeNode: Category;

  updateHasChild(_: number, node: Category): boolean {
    return !!node.subCategories && node.subCategories.length > 0;
  }

  hasChild = (_: number, node: Category) => !!node.subCategories && node.subCategories.length > 0;


  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

  selectCategory(selectedCategory: Category) {
    console.info(selectedCategory.id);
    const dialogRef = this.dialog.open(DialogContentExampleDialog,
      {
        data: { category:selectedCategory }
      });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });

  }

}

@Component({
  selector: 'dialog-content-example-dialog',
  templateUrl: 'dialog-content-example-dialog.html',
})
export class DialogContentExampleDialog {
  constructor(private router: Router,
    public dialogRef: MatDialogRef<DialogContentExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { category: Category }) {

  }

  startQuiz() {
    this.router.navigateByUrl(`/quiz/category/${this.data.category.id}`);
    }

}

