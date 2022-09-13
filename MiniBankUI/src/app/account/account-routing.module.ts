import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { ActionsComponent } from './actions/actions.component';
import { LoginComponent } from './login/login.component';
import { OpenAccountComponent } from './open-account/open-account.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { TransactionComponent } from './transaction/transaction.component';

const routes: Routes =
[
  {path:'',redirectTo:'login',pathMatch:'full'},
  {path:'open-account',component:OpenAccountComponent},
  {path:'actions',component:ActionsComponent},
  {path:'account-details',component:AccountDetailsComponent},
  {path:'transaction',component:TransactionComponent},
  {path:'login',component:LoginComponent},
  {path:'operationHistory',component:OperationsHistoryComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
