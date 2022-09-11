import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Transaction } from 'src/app/models/transaction.model';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  form!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private transactionService: TransactionService,
   // private alertService: AlertService
  ) { }

ngOnInit() {
  this.form = this.formBuilder.group({
    fromAccountId: ['', Validators.required],
    toAccountId: ['', Validators.required],
    amount:['',Validators.required,Validators.min(1),Validators.max(1000000)]
  });


}
 onSubmit()
 {
    this.submitted = true;

    // reset alerts on submit
    //this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;

    let trans=this.form.value as Transaction;
    let accountId=sessionStorage.getItem('accountId');
    accountId?trans.fromAccountId=accountId:this.router.navigateByUrl('account/logins')


    this.transactionService.createTransaction(this.form.value as Transaction)
        .subscribe(
            (isAdded: boolean) => {
              alert(`Your request has been accepted in the system. The transfer will be made as soon as possible `)
                this.loading = false;
            },
            (error: HttpErrorResponse) => {
              //   this.alertService.error(error.message);
                this.loading = false;
            });
  }

// convenience getter for easy access to form fields
get f() { return this.form.controls; }
}
