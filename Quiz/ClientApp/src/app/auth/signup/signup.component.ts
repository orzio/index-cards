import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { EMPTY, Subject, Subscription } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit, OnDestroy {

  constructor(private authService: AuthService, private router: Router) { }

  private subscriptions = new Subscription();

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  signUpForm = new FormGroup({
    email: new FormControl(),
    firstName: new FormControl(),
    lastName: new FormControl(),
    password: new FormControl()
  })

  ngOnInit() {
  }

  onSubmit() {
    let signUp = {
      email: this.signUpForm.value.email,
      firstName: this.signUpForm.value.firstName,
      lastName: this.signUpForm.value.lastName,
      password: this.signUpForm.value.password,
    } as SignUpData

    let sub = this.authService.signUp(signUp).subscribe(
      () => {
        console.log("User is logged in");
        this.router.navigateByUrl('signin');
      },
      error => {
        this.errorMessageSubject.next(error)
      }
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
