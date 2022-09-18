import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/models/login.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService  {

  isUserLoggedIn: boolean = false;
  accountId: string = '';

  constructor(private _http: HttpClient) { }

  login(login:Login): Observable<string> {
    return this._http.post<string>("api/Login",login).pipe(
      map((accountId: string) => {
        this.isUserLoggedIn = true;
        this.accountId = accountId;
        return accountId;
      })
    );
  }
}
