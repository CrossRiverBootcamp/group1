import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerAccountService } from '../services/customer-account.service';


@Component({
  selector: 'app-open-account',
  templateUrl: './open-account.component.html',
  styleUrls: ['./open-account.component.scss']
})
export class OpenAccountComponent implements OnInit {

  accountDetailsForm!: FormGroup;
  verificationForm!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private accountService: CustomerAccountService
   // private alertService: AlertService
  ) { }

  ngOnInit() {
    this.accountDetailsForm = this.formBuilder.group({
      email: ['', Validators.required, Validators.email],
      password: ['', [Validators.required, Validators.minLength(4)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });

    this.verificationForm=this.formBuilder.group({
      verificationCode:['',[Validators.required, Validators.minLength(5),Validators.maxLength(5)]]
    })

  }

  // convenience getter for easy access to accountDetailsForm fields
  get f() { return this.accountDetailsForm.controls; }

  // convenience getter for easy access to verificationForm fields
  get f2() { return this.accountDetailsForm.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    //this.alertService.clear();

    // stop here if accountDetailsForm is invalid
    if (this.accountDetailsForm.invalid) {
      return;
    }
  }

  // onSubmit() {
  //   this.submitted = true;

  //   // reset alerts on submit
  //   //this.alertService.clear();

  //   // stop here if accountDetailsForm is invalid
  //   if (this.accountDetailsForm.invalid) {
  //     return;
  //   }

  //   this.loading = true;
  //   this.accountService.createCustomerAccount(this.accountDetailsForm.value)
  //     .subscribe(
  //       (isAdded: boolean) => {
  //         isAdded?alert('added'):alert("an error occurred, please try again"); this.loading = false;
  //         //this.alertService.success('Registration successful', { keepAfterRouteChange: true });
  //         this.router.navigateByUrl('account/login');
  //       },
  //       (error: HttpErrorResponse) => {
  //         console.log(error);
  //         //this.alertService.error(error.message);
  //         this.loading = false;
  //       });
  // }
}
