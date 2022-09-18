import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Customer } from 'src/app/models/customer.model';
import { CustomerAccountService } from '../../../services/customer-account.service';
import { EmailVerificationService } from '../../../services/email-verification.service';

const ATTEMPTS_ALLOWED = 4;
//const NUM_OF_VERIFY_CODES_ALLOWED=2;


@Component({
  selector: 'app-open-account',
  templateUrl: './open-account.component.html',
  styleUrls: ['./open-account.component.scss']
})
export class OpenAccountComponent implements OnInit {

  accountDetailsForm!: FormGroup;
  verificationForm!: FormGroup;
  accountDetailsViewForm!: FormGroup;
  loading = false;
  accountDetailsFormSubmitted = false;
  // verificationFormSubmitted=false;

  userVerified: boolean = true;
  emailExists: boolean = false;
  numOfAttempts: number = 0;
  NumOfVerificationCodesSent: number = 0;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private accountService: CustomerAccountService,
    private emailVerificationService: EmailVerificationService
    // private alertService: AlertService
  ) { }

  ngOnInit() {
    this.accountDetailsForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });

    this.verificationForm = this.formBuilder.group({
      verificationCode: ['', [Validators.required
        // , Validators.pattern("[0-9]{6}")
      ]]
    })

    this.accountDetailsViewForm = this.formBuilder.group({});

  }

  // convenience getter for easy access to accountDetailsForm fields
  get f() { return this.accountDetailsForm.controls; }

  // convenience getter for easy access to verificationForm fields
  get f2() { return this.verificationForm.controls; }

  getVerificationCodeByEmail(isResend: boolean) {
    if (this.accountDetailsForm.invalid) {
      return;
    }
    this.accountDetailsFormSubmitted = true;
    this.accountDetailsForm.disable();
    //this.NumOfVerificationCodesSent++;
    this.emailVerificationService.sendEmailVerification(this.f['email'].value, isResend)
      .subscribe(() => {
        alert("verification code sent. check you email")
      },
        (error) => {
          alert("can not resend a code... sorry:(")
          window.location.reload()
        }
      );
  }

  checkIfEmailExists() {
    this.emailExists = false;
    if (this.f['email'].invalid) {
      return;
    }
    this.accountService.customerExists(this.accountDetailsForm.get('email')?.value)
      .subscribe((res: boolean) => {
        if (res) {
          this.emailExists = true;
        }
      })
  }

  OpenNewAccount() {
    this.loading = true;
    let customer = this.accountDetailsForm.value as Customer;
    customer.verificationCode = this.verificationForm.get('verificationCode')?.value;
    this.accountService.createCustomerAccount(customer)
      .subscribe(
        (isAdded: boolean) => {
          if (isAdded) {
            alert("Welcome!! please login");
            this.router.navigateByUrl('login');
          }
          else {
            alert("ooops error acured, please try again");
            window.location.reload()
          }
          this.loading = false;
          //this.alertService.success('Registration successful', { keepAfterRouteChange: true });

        },
        (error: HttpErrorResponse) => {
          switch (error.status) {
            case 401:
              {
                {
                  alert(`wrong code. you have ${ATTEMPTS_ALLOWED - (this.numOfAttempts++)} attempts left`);
                }
              }
              break;
            case 417: alert('code expired. Try a resend requst');
              break;
            case 429: {
              alert(`wrong code. no more attempts`)
              window.location.reload()
            }
              break;
            default:
              {
                alert('Unresolved error:( Please try again later..');
                window.location.reload()
              }
          }
          console.log(error);
          //this.alertService.error(error.message);
          this.loading = false;
        });
  }

  onSubmit() {

    //this.submitted = true;
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
}
