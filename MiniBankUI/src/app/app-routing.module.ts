import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes =
[
  {path:"account",loadChildren:()=>import("./modules/account/account-routing.module")
    .then(m=>m.AccountRoutingModule)},
  {path:"",loadChildren:()=>import('./modules/authentication/authentication-routing.module')
  .then(m=>m.AuthenticationRoutingModule),pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
