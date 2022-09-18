import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { TransactionComponent } from './transaction/transaction.component';

const routes: Routes = [
  {path:'transaction',component:TransactionComponent},
  {path:'operationHistory',component:OperationsHistoryComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OperationsRoutingModule { }
