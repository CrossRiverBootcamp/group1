import { Component, OnInit } from '@angular/core';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { OperationsHistoryService } from '../../../services/operations-history.service';
import { OperationData } from 'src/app/models/operation-data';
import { TransactionPartner } from 'src/app/models/transaction-partner';
import { MatDialog } from '@angular/material/dialog';
import { DialogTransactionPartnerDetailsComponent } from '../dialog-transaction-partner-details/dialog-transaction-partner-details.component';
import { MatTableDataSource } from '@angular/material/table';
import { AuthenticationService } from '../../../services/authentication.service';

export interface DialogTransactionPartnerDetailsData {
  transactionPartner: TransactionPartner;
}

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.scss']
})

export class OperationsHistoryComponent implements OnInit {
  displayedColumns: string[] = ['Credit/Debit', 'From whom/to whom', 'Amount', 'Balance', 'Date'];
  data: OperationData[] = [];
  dataSource = new MatTableDataSource<OperationData>(this.data);

  resultsLength: number = 0;
  isLoadingResults: boolean = true;
  currentPage: number = 0;
  numOfRecords: number = 3;
  pageSizeOptions: number[] = [3, 5];
  accountId: string;
  transactionPartner?: TransactionPartner;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private _operationsHistoryService: OperationsHistoryService, private _authenticationService: AuthenticationService, private _dialog: MatDialog,
  ) {
    this.accountId = _authenticationService.currentUserValue.accountId;
  }

  openTransactionPartnerDetailsDialog(): void {
    const RBdialogRef = this._dialog.open(DialogTransactionPartnerDetailsComponent, {
      width: '20%',
      height: '50%',
      data: {
        transactionPartner: this.transactionPartner
      },
    });

    RBdialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  ngOnInit(): void {
    this.getCountOperations();
    this.loadOperations();

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  getCountOperations() {
    this._operationsHistoryService.getCountOperations(this.accountId).subscribe(res => {
      this.resultsLength = res;
    });
  }

  loadOperations() {
    this.isLoadingResults = true;
    this._operationsHistoryService.getOperationsHistory(
      this.accountId!,
      this.currentPage,
      this.numOfRecords
    ).subscribe(result => {
      this.data = result;
      this.dataSource = new MatTableDataSource<OperationData>(this.data);
      this.isLoadingResults = false;
      this.paginator.pageIndex = this.currentPage;
    }, err => {
      this.isLoadingResults = false;
    })
  }
  pageChanged(event: PageEvent) {
    this.currentPage = event.pageIndex;
    this.numOfRecords = event.pageSize;

    this.loadOperations();
  }

  viewTransactionPartnerDetails(transactionPartnerAccountId: string) {
    this.transactionPartner = this._operationsHistoryService.getTransactionPartnerByAccountId(transactionPartnerAccountId);
    if (!this.transactionPartner) {
      this._operationsHistoryService.getTransactionPartnerDetails(transactionPartnerAccountId).subscribe(res => {
        this.transactionPartner = res;
        this.transactionPartner.accountId = transactionPartnerAccountId;
        this._operationsHistoryService.addTransactionPartner(res);
        this.openTransactionPartnerDetailsDialog();
      })
    }
    else
      this.openTransactionPartnerDetailsDialog();
  }
}
