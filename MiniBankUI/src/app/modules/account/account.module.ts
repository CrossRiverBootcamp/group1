import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OpenAccountComponent } from './open-account/open-account.component';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { TransactionComponent } from './transaction/transaction.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { ActionsComponent } from './actions/actions.component';
import { DialogTransactionPartnerDetailsComponent } from './dialog-transaction-partner-details/dialog-transaction-partner-details.component';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';


@NgModule({
  declarations: [
    OpenAccountComponent,
    ActionsComponent,
    AccountDetailsComponent,
    TransactionComponent,
    OperationsHistoryComponent,
    DialogTransactionPartnerDetailsComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    AccountRoutingModule
  ],
  exports:[
    OpenAccountComponent,
    ActionsComponent,
    AccountDetailsComponent,
    OperationsHistoryComponent,
    TransactionComponent,
    DialogTransactionPartnerDetailsComponent
  ]
})
export class AccountModule { }
