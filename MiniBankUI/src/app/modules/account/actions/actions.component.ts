import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss']
})
export class ActionsComponent implements OnInit {
  
  @Output() viewActionsNav = new EventEmitter();

  constructor(private router:Router) { }

  ngOnInit(): void {
    this.viewActionsNav.emit();
  }

  openTransactionView(){
    this.viewActionsNav.emit();
    this.router.navigateByUrl('account/transaction')
  }

    openOperationsHistory(){
      this.viewActionsNav.emit();
    this.router.navigateByUrl('account/operationHistory')
  }
}
