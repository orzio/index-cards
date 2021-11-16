import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  constructor(private authService: AuthService, private router:Router) { }

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

    this.authService.signUp(signUp).subscribe(
      () => {
        console.log("User is logged in");
        this.router.navigateByUrl('signin');
      }
    );
  }
}
