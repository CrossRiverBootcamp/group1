import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TransactionPartner } from 'src/app/models/transaction-partner';
import { DialogTransactionPartnerDetailsData } from '../operations-history/operations-history.component';

@Component({
  selector: 'app-dialog-transaction-partner-details',
  templateUrl: './dialog-transaction-partner-details.component.html',
  styleUrls: ['./dialog-transaction-partner-details.component.scss']
})
export class DialogTransactionPartnerDetailsComponent implements OnInit {

  transactionPartner!: TransactionPartner;

  constructor(
    public dialogRef: MatDialogRef<DialogTransactionPartnerDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogTransactionPartnerDetailsData,
  ) { }

  ngOnInit(): void {
    this.transactionPartner = this.data.transactionPartner;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
