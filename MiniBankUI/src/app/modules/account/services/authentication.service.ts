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

  private currentUserSubject: BehaviorSubject<LoginReturn | any>;
  public currentUser: Observable<LoginReturn>;

  constructor(private http: HttpClient) {
    let user=localStorage.getItem('currentUser');
    user?this.currentUserSubject = new BehaviorSubject<LoginReturn>(JSON.parse(user)):
    this.currentUserSubject = new BehaviorSubject<any>(null);
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): LoginReturn {
      return this.currentUserSubject.value;
  }

  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
  }

  login(login:Login): Observable<LoginReturn> {
    return this.http.post<LoginReturn>("api/Login",login).pipe(
      map((login: LoginReturn) => {
        localStorage.setItem('currentUser', JSON.stringify(login));
        this.currentUserSubject.next(login);
        return login;
      })
    );
  }
}
