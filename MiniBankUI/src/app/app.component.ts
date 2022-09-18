import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './account/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MiniBankUI';

  viewActionsNav: boolean = false;

  constructor(private router: Router, private _authenticatioService:AuthenticationService) { }

  changeViewActionsNav() {
    this.viewActionsNav = !this.viewActionsNav;
  }

  openActions() {
    this.router.navigateByUrl('account/actions')
  }

  checkIfUserLoggedIn() {
    return this._authenticatioService.isUserLoggedIn;
  }

  // openTransactionView(){
  //   this.router.navigateByUrl('account/transaction')
  // }

  // openOperationsHistory(){
  //   this.router.navigateByUrl('account/operationHistory')
  // }
}
