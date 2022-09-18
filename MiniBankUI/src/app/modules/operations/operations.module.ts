import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperationsRoutingModule } from './operations-routing.module';
import { DialogTransactionPartnerDetailsComponent } from './dialog-transaction-partner-details/dialog-transaction-partner-details.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { TransactionComponent } from './transaction/transaction.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [TransactionComponent,
    OperationsHistoryComponent,
    DialogTransactionPartnerDetailsComponent,],
  imports: [
    CommonModule,
    OperationsRoutingModule,
    SharedModule
  ], exports: [TransactionComponent,
    OperationsHistoryComponent,
    DialogTransactionPartnerDetailsComponent,],
})
export class OperationsModule { }
