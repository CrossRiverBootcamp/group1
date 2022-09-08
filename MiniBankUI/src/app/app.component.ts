import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MiniBankUI';
  /**
   *
   */
  constructor(private router:Router) {  }
  openTransactionView(){
    this.router.navigateByUrl('account/transaction')
  }
}
