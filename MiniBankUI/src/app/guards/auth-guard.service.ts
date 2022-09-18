import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticationService } from '../account/services/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor( private authenticationService: AuthenticationService, private router: Router) {
  }
  canActivate() {
    const currentUser = this.authenticationService.currentUserValue;
    if (currentUser) {
        // logged in so return true
        return true;
    }
    // not logged in so redirect to login page with the return url
    this.router.navigate(['account/login']);
    return false;
  }
}