import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OpenAccountComponent } from './open-account/open-account.component';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { ActionsComponent } from './actions/actions.component';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';


@NgModule({
  declarations: [
    OpenAccountComponent,
    ActionsComponent,
    AccountDetailsComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    AccountRoutingModule
  ],
  exports:[
    OpenAccountComponent,
    ActionsComponent,
    AccountDetailsComponent,
  ]
})
export class AccountModule { }
