import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OpenAccountComponent } from './open-account/open-account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { LoginComponent } from './login/login.component';



@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,   FormsModule,
    ReactiveFormsModule,
  ]
})
export class AccountModule { }
