import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerAccount } from 'src/app/models/customer-account.model';
import { Customer } from 'src/app/models/customer.model';

@Injectable({
  providedIn: 'root'
})

export class CustomerAccountService {

  constructor(private _http: HttpClient) { }

  get(accountId:string): Observable<CustomerAccount>{
    return this._http.get<Boolean>("api/Account",accountId);
  }

  login(customer:Customer): Observable<Boolean> {
    return this._http.post<Boolean>("api/Account",customer);
  }
}
