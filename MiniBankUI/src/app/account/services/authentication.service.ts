import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/models/login.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService  {

  private currentUserSubject: BehaviorSubject<LoginReturned>;
  public currentUser: Observable<LoginReturned>;

  constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<LoginReturned>(JSON.parse(localStorage.getItem('currentUser')||''));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): LoginReturned {
      return this.currentUserSubject.value;
  }

  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
  }

  login(login:Login): Observable<string> {
    return this.http.post<string>("api/Login",login).pipe(
      map((login: LoginReturned) => {
        localStorage.setItem('currentUser', JSON.stringify(login));
        this.currentUserSubject.next(login);
        return login;
      })
    );
  }
}
