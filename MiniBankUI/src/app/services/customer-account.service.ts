import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerAccount } from 'src/app/models/customer-account.model';
import { Customer } from 'src/app/models/customer.model';

@Injectable({
  providedIn: 'root'
})

export class CustomerAccountService {
  customerExists(email:string) {
    return this._http.get<boolean>(`api/Account/${email}/Exists`);
  }

  accountExists(accountId:string) {
    return this._http.get<boolean>(`api/Account/${accountId}/AccountExists`);
  }

  constructor(private _http: HttpClient) { }

  getAccountInfo(accountId:string): Observable<CustomerAccount>{
    return this._http.get<CustomerAccount>(`api/Account/${accountId}`);
  }

  createCustomerAccount(customer:Customer): Observable<boolean> {
    return this._http.post<boolean>("api/Account",customer);
  }
}
