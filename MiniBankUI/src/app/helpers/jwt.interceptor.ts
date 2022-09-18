import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../modules/account/services/authentication.service';




@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private _authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to the api url
        const currentUser = this._authenticationService.currentUserValue;
        const isLoggedIn = currentUser && currentUser.token;
        // const isApiUrl = request.url.startsWith(environment.apiUrl);
        // if (isLoggedIn && isApiUrl) {
          if(isLoggedIn)
          request = request.clone({
              setHeaders: {
                  Authorization: `Bearer ${currentUser.token}`
              }
          });

        return next.handle(request);
    }
}
