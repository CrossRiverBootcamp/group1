import { NgModule } from '@angular/core';
import {MatStepperModule} from '@angular/material/stepper';
import {MatButtonModule} from '@angular/material/button';
import { NgOtpInputModule } from 'ng-otp-input';
import {MatDialogModule} from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';


@NgModule({
  declarations: [],
  imports: [
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatStepperModule,
    MatButtonModule,
    NgOtpInputModule,
    MatDialogModule
  ],
  exports:[
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatStepperModule,
    MatButtonModule,
    NgOtpInputModule,
    MatDialogModule
  ]
})
export class SharedModule { }
