import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit, OnDestroy {

  constructor(private authService: AuthService, private router: Router) { }

  private subscriptions = new Subscription();
  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  ngOnInit() { }

  signInForm = new FormGroup({
    userName: new FormControl("test@test.com"),
    password: new FormControl("Secret123!")
  })

  onSubmit() {
    let signInData = {
      email: this.signInForm.value.userName,
      password: this.signInForm.value.password
    } as SignInData

    console.log("sudsfmit");
    let sub = this.authService.signIn(signInData).subscribe(
      () => {
        this.router.navigateByUrl('home');
      },
      error => {
        this.errorMessageSubject.next(error)
      }
    );

    this.subscriptions.add(sub);
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }


}
