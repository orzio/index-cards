import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuizErrorHandlerService{
  //https://dev.to/buildmotion/angular-errorhandler-to-handle-or-not-to-handle-1e7l
  constructor() { }

  public handleError(err: any) {
    console.log("Error handler");
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      if(err.error===undefined || !!err.error)
        errorMessage = `${JSON.stringify(err)}`;
      else
      errorMessage = `${err.error}`;
    }
    return throwError(errorMessage);
  }
}


