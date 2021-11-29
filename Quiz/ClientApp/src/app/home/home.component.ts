import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { Inject, OnDestroy, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeNestedDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CategoryService } from '../services/category-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  treeControl = new NestedTreeControl<Category>(node => node.subCategories);
  dataSource = new MatTreeNestedDataSource<Category>();
  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  private categories$ = this.categoryService.quizCategories$.pipe(catchError(error => {
    this.errorMessageSubject.next(error);
    return EMPTY;
  }));
  private subscriptions = new Subscription();

  constructor(private categoryService: CategoryService, public dialog: MatDialog) { }
  ngOnInit(): void {
    let sub = this.categories$.subscribe((data: Category[]) => {
      this.dataSource.data = data
    },
      error => {
        this.errorMessageSubject.next(error)
      });
    this.subscriptions.add(sub);
  }

  activeNode: Category;

  updateHasChild(_: number, node: Category): boolean {
    return !!node.subCategories && node.subCategories.length > 0;
  }

  hasChild = (_: number, node: Category) => !!node.subCategories && node.subCategories.length > 0;

  selectCategory(selectedCategory: Category) {
    console.info(selectedCategory.id);
    const dialogRef = this.dialog.open(DialogContentExampleDialog,
      {
        data: { category: selectedCategory }
      });

    let sub = dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    }, error => {
      this.errorMessageSubject.next(error)
    });
    this.subscriptions.add(sub);
  }



  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
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

