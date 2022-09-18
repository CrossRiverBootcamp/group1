import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/models/login.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { LoginReturn } from 'src/app/models/loginReturn.model';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {

  isUserLoggedIn: boolean = false;
  accountId: string = '';

  constructor(private _http: HttpClient) { }
  
  login(login:Login): Observable<LoginReturn> {
    return this._http.post<LoginReturn>("api/Login",login).pipe(
      map((loginReturn: LoginReturn) => {
        this.isUserLoggedIn = true;
        this.accountId = loginReturn.accountId;
        return loginReturn;
      })
    );
  }
}
