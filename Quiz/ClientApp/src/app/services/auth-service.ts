import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';

import { combineLatest, Subject, throwError } from 'rxjs';

import { tap, catchError, shareReplay, map } from 'rxjs/operators';

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
      .pipe(tap(res => this.setSession(res)));

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

  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
}

  isLoggedIn():boolean {
    return this.currentUserSubject.value !=null;
  }


  signUp(signUpData: SignUpData) {
    return this.httpClient.post(`${this.authUrl}/signup`, signUpData, this.httpOptions);
  }

  constructor(private httpClient: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  private handleError(err: any) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }

}


//https://angular.io/guide/http
