import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerAccount } from 'src/app/models/customer-account.model';
import { Customer } from 'src/app/models/customer.model';

@Injectable({
  providedIn: 'root'
})

export class EmailVerificationService {

  constructor(private _http: HttpClient) { }

  createEmailVerification(email:string):Observable<void>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
  });
    return this._http.post<void>("api/EmailVerification",JSON.stringify(email), {headers: headers});
  }

  resendEmailVerificationCode(email:string){
    return this._http.put("api/EmailVerification",email);
  }
}
