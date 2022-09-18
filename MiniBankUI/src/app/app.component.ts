import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MiniBankUI';

  viewActionsNav: boolean = false;

  constructor(private router: Router, private _authenticationService:AuthenticationService) { }

  changeViewActionsNav() {
    this.viewActionsNav = !this.viewActionsNav;
  }

  openActions() {
    this.router.navigateByUrl('account/actions')
  }

  showDiv() {
    return this._authenticationService.isLogged&&this._authenticationService.isInActions;
  }

}
