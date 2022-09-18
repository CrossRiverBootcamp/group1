import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { ActionsComponent } from './actions/actions.component';
import { OpenAccountComponent } from './open-account/open-account.component';


const routes: Routes =
[
  {path:'open-account',component:OpenAccountComponent},
  {path:'actions',component:ActionsComponent,loadChildren:()=>import('../operations/operations-routing.module')
  .then(m=>m.OperationsRoutingModule)},
  {path:'account-details',component:AccountDetailsComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
