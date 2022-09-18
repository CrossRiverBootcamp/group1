import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgOtpInputComponent } from 'ng-otp-input';
import { Customer } from 'src/app/models/customer.model';
import { CustomerAccountService } from '../../../services/customer-account.service';
import { EmailVerificationService } from '../../../services/email-verification.service';

const ATTEMPTS_ALLOWED=4;


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

  userVerified: boolean = true;
  emailExists: boolean = false;
  numOfAttempts: number = 0;
  NumOfVerificationCodesSent: number = 0;

  @ViewChild(NgOtpInputComponent, { static: false}) ngOtpInput:NgOtpInputComponent | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private accountService: CustomerAccountService,
    private emailVerificationService: EmailVerificationService
  ) { }

  ngOnInit() {

    this.ngOtpInput?.otpForm.disable();

    this.accountDetailsForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
    });

    this.verificationForm=this.formBuilder.group({
      verificationCode:['',[Validators.required]]
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
    this.emailVerificationService.sendEmailVerification(this.f['email'].value,isResend)
      .subscribe(()=>{
        this.ngOtpInput?.otpForm.enable();
      },
      (error)=>{
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

        },
        (error: HttpErrorResponse) => {
          switch (error.status) {
            case 401:
                  {
                    alert(`wrong code. you have ${ATTEMPTS_ALLOWED-(this.numOfAttempts++)} attempts left`);
                    this.ngOtpInput?.setValue('');
                    break;
                  }
              case 417:alert('code expired. Try a resend request');
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
          this.loading = false;
        });
  }

}
