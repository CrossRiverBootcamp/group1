import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { LoginComponent } from './login/login.component';
import { OpenAccountComponent } from './open-account/open-account.component';

const routes: Routes = [{path:'open-account',component:OpenAccountComponent},{path:'account-details',component:AccountDetailsComponent},{path:'login',component:LoginComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
