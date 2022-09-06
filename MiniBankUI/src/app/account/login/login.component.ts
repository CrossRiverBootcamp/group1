import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login.model';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private loginService: LoginService,
   // private alertService: AlertService
  ) { }

ngOnInit() {
  this.form = this.formBuilder.group({
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required]
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
    this.loginService.login(this.form.value as Login)
        .subscribe(
            (accountId: string) => {
                sessionStorage.setItem('accountId', accountId);
                this.loading = false;
                this.router.navigate(['account-details']);
            },
            (error: HttpErrorResponse) => {
              //   this.alertService.error(error.message);
                this.loading = false;
            });
  }



// convenience getter for easy access to form fields
get f() { return this.form.controls; }
}
