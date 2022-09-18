import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth-guard.service';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { ActionsComponent } from './actions/actions.component';
import { OpenAccountComponent } from './open-account/open-account.component';


const routes: Routes =
[
  {path:'open-account',component:OpenAccountComponent},
  {path:'actions',component:ActionsComponent, canActivate: [AuthGuard],loadChildren:()=>import('../operations/operations-routing.module')
  .then(m=>m.OperationsRoutingModule)},
  {path:'account-details', canActivate: [AuthGuard],component:AccountDetailsComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
