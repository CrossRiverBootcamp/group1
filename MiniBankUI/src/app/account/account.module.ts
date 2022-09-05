import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OpenAccountComponent } from './open-account/open-account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    OpenAccountComponent
  ],
  imports: [
    CommonModule,   FormsModule,
    ReactiveFormsModule,
  ]
})
export class AccountModule { }
