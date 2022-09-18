import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss']
})
export class ActionsComponent implements OnInit, OnDestroy{

  @Output() viewActionsNav = new EventEmitter();

  constructor(private router:Router, private _authenticationService:AuthenticationService) {
    this._authenticationService.isInActions=true;
  }
  ngOnDestroy(): void {
    this._authenticationService.isInActions=false;
  }

  ngOnInit(): void {
    this.viewActionsNav.emit();
  }

  openTransactionView(){
    this.viewActionsNav.emit();
    this.router.navigate(['./transaction'])
  }

    openOperationsHistory(){
      this.viewActionsNav.emit();
      this.router.navigate(['./operation-history'])
  }
}
