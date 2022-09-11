import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'src/app/models/login.model';
import { Observable } from 'rxjs';
import { Transaction } from 'src/app/models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {


  constructor(private _http: HttpClient) { }

  createTransaction(transaction:Transaction): Observable<boolean> {
    return this._http.post<boolean>("api/Transaction",transaction);
  }
}
