import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  signInForm = new FormGroup({
    userName: new FormControl(),
    password: new FormControl()
  })
  ngOnInit() {
  }

  onSubmit() {
    let signInData = {
      email: this.signInForm.value.userName,
      password: this.signInForm.value.password
    } as SignInData

    this.authService.signIn(signInData).subscribe(
      () => {
        console.log("User is logged in");
        this.router.navigateByUrl('home');
      }
    );
  }

}
