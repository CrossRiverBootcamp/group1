import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Transaction } from 'src/app/models/transaction.model';
import { CustomerAccountService } from 'src/app/services/customer-account.service';
import { AuthenticationService } from '../../../services/authentication.service';
import { TransactionService } from '../../../services/transaction.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  form!: FormGroup;
  loading = false;
  submitted = false;
  toAccountIdExists: boolean = true;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private transactionService: TransactionService,
    private authenticationService: AuthenticationService,
    private accountService: CustomerAccountService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      toAccountId: ['', [Validators.required, Validators.pattern("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$")]],
      amount: ['', [Validators.required, Validators.min(1), Validators.max(1000000)]]
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    let trans = this.form.value as Transaction;
    let accountId = this.authenticationService.currentUserValue.accountId;
    accountId ? trans.fromAccountId = accountId : this.router.navigateByUrl('account/login')

    this.transactionService.createTransaction(this.form.value as Transaction)
      .subscribe(
        (isAdded: boolean) => {
          this.loading = false;
          alert(`Your request has been accepted in the system. The transfer will be made as soon as possible `)
          this.router.navigateByUrl('account/actions');
        },
        (error: HttpErrorResponse) => {
          this.loading = false;
          alert(`error of type ${error.statusText} ocurred `);
          this.form.reset();
        });
  }

  checkIfToAccountIdExists() {
    this.toAccountIdExists = true;
    if (this.f['toAccountId'].invalid) {
      return;
    }
    this.accountService.accountExists(this.form.get('toAccountId')?.value)
      .subscribe(
        ((res: boolean) => {
          if (res) {
            this.toAccountIdExists = true;
          }
          else {
            this.toAccountIdExists = false;
          }
        }), (err: HttpErrorResponse) =>
        this.f['toAccountId'].reset()
      )
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }
}
