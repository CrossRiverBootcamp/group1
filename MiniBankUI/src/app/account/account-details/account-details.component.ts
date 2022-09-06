import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CustomerAccountInfo } from 'src/app/models/customer-account.model';
import { CustomerAccountService } from '../services/customer-account.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent  {

  accountInfo!:CustomerAccountInfo;
  constructor(private accountService:CustomerAccountService,
    // private alertService: AlertService
    ) {
      let accountId= sessionStorage.getItem('accountId');
      if(accountId)
        accountService.getAccountInfo(accountId)
        .subscribe((accountInfo:CustomerAccountInfo) => {this.accountInfo = accountInfo}, (error: HttpErrorResponse) => {
            //this.alertService.error(error.message);
        });
   }

}
