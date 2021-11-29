import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';

import { combineLatest, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map } from 'rxjs/operators';
import { QuizErrorHandlerService } from './error-handlers/quiz-error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  private jwtHelper = new JwtHelperService();
  private authUrl = 'auth';
  private xmlSoapNamespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
  private microsoftNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims/";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  signIn(signInData: SignInData) {
    return this.httpClient.post<string>(`${this.authUrl}/signin`, signInData, this.httpOptions)
      .pipe(tap(res => this.setSession(res)),
        catchError(this.errorHandlerService.handleError)
      );
  }

  private setSession(token) {
    const decodedToken = this.jwtHelper.decodeToken(token.token);

    let user = {
      email: decodedToken[`${this.xmlSoapNamespace}name`],
      id: decodedToken[`${this.xmlSoapNamespace}nameidentifier`],
      role: decodedToken[`${this.microsoftNamespace}role`],
      token: token.token,
    } as User;

    localStorage.setItem('token', token.token);
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): boolean {
    return this.currentUserSubject.value != null;
  }


  signUp(signUpData: SignUpData) {
    return this.httpClient.post(`${this.authUrl}/signup`, signUpData, this.httpOptions).pipe(
      catchError(this.errorHandlerService.handleError)
    );
  }

  constructor(private httpClient: HttpClient, private errorHandlerService: QuizErrorHandlerService) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }



}


//https://angular.io/guide/http
