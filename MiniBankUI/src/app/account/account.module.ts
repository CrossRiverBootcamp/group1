import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OpenAccountComponent } from './open-account/open-account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { LoginComponent } from './login/login.component';
import { TransactionComponent } from './transaction/transaction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { ActionsComponent } from './actions/actions.component';
import { DialogTransactionPartnerDetailsComponent } from './dialog-transaction-partner-details/dialog-transaction-partner-details.component';
import {MatStepperModule} from '@angular/material/stepper';
import {MatButtonModule} from '@angular/material/button';
import { NgOtpInputModule } from 'ng-otp-input';



@NgModule({
  declarations: [
    OpenAccountComponent,
    ActionsComponent,
    AccountDetailsComponent,
    LoginComponent,
    TransactionComponent,
    OperationsHistoryComponent,
    DialogTransactionPartnerDetailsComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatStepperModule,
    MatButtonModule,
    NgOtpInputModule
  ],
  exports:[
    OpenAccountComponent,
    ActionsComponent,
    AccountDetailsComponent,
    LoginComponent,
    OperationsHistoryComponent,
    TransactionComponent,
    DialogTransactionPartnerDetailsComponent
  ]
})
export class AccountModule { }
