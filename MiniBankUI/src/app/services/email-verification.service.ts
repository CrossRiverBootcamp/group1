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


  sendEmailVerification(email:string,isResendRequest:boolean):Observable<void>{
    return this._http.post<void>(`api/EmailVerification?isResendRequest=${isResendRequest}`,JSON.stringify(email),
     {headers:new HttpHeaders({
    'Content-Type': 'application/json'})
   });
  }

}
