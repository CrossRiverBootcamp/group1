import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { Observable } from 'rxjs';
import { OperationData } from 'src/app/models/operation-data';
import { TransactionPartner } from 'src/app/models/transaction-partner';

@Injectable({
  providedIn: 'root'
})

export class OperationsHistoryService {

  transactionPartners:TransactionPartner[]=[];

  constructor(private _http: HttpClient) { }

  getTransactionPartnerByAccountId(accountId:string)
  {
     return this.transactionPartners.find(tp=> tp.accountId = accountId);
  }

  addTransactionPartner(transactionPartner: TransactionPartner)
  {
    this.transactionPartners.push(transactionPartner);
  }

  getCountOperations(accountId:string): Observable<number> {
    return this._http.get<number>
      (`api/Operation/${accountId}/getCountOperations`);
  }

  getOperationsHistory(accountId:string, PageNumber: number, PageSize:number): Observable<OperationData[]> {
    return this._http.get<OperationData[]>
      (`api/Operation/${accountId}/getOperations?PageNumber=${PageNumber + 1}&PageSize=${PageSize}`);
  }

  getTransactionPartnerDetails(accountId:string): Observable<TransactionPartner> {
    return this._http.get<TransactionPartner>(`api/Operation/${accountId}/transactionPartnerAccountId`);
  }

}
