import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OpenAccountComponent } from './open-account/open-account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { LoginComponent } from './login/login.component';
import { TransactionComponent } from './transaction/transaction.component';



@NgModule({
  declarations: [
    OpenAccountComponent,
    AccountDetailsComponent,LoginComponent, TransactionComponent
  ],
  imports: [
    CommonModule,   FormsModule,
    ReactiveFormsModule,
  ],
  exports:[
    OpenAccountComponent,
    AccountDetailsComponent,LoginComponent
  ]
})
export class AccountModule { }
