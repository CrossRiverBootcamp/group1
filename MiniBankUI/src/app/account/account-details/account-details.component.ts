import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CustomerAccountInfo } from 'src/app/models/customer-account.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.scss']
})
export class AccountDetailsComponent  {

  accountInfo!:CustomerAccountInfo;
  constructor(private accountService:AccountService,
    // private alertService: AlertService
    ) {
      accountService.getAccountInfo()
      .subscribe((accountInfo:CustomerAccountInfo) => {this.accountInfo = accountInfo}, (error: HttpErrorResponse) => {
          //this.alertService.error(error.message);
      });
   }

}
