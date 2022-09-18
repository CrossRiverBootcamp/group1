import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes =
[
  {path:"account",loadChildren:()=>import("./account/account-routing.module")
    .then(m=>m.AppRoutingModule)},
  {path:'',redirectTo:'account',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
