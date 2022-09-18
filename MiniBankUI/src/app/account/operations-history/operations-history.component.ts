import { Component, OnInit } from '@angular/core';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { OperationsHistoryService } from '../services/operations-history.service';
import { OperationData } from 'src/app/models/operation-data';
import { TransactionPartner } from 'src/app/models/transaction-partner';
import { MatDialog } from '@angular/material/dialog';
import { DialogTransactionPartnerDetailsComponent } from '../dialog-transaction-partner-details/dialog-transaction-partner-details.component';
import { LoginService } from '../services/login.service';
import { MatTableDataSource } from '@angular/material/table';

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
  //exampleDatabase?: ExampleHttpDatabase;
  data: OperationData[] = [];
  dataSource = new MatTableDataSource<OperationData>(this.data);

  resultsLength: number = 0;
  isLoadingResults: boolean = true;
  //isRateLimitReached: boolean = false;
  currentPage: number = 0;
  numOfRecords: number = 5;
  accountId: string;
  //accountId = sessionStorage.getItem('accountId');
  transactionPartner?: TransactionPartner;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  //@ViewChild(MatSort) sort!: MatSort;

  constructor(private _operationsHistoryService: OperationsHistoryService, private _loginService: LoginService, private _dialog: MatDialog,
  ) {
    this.accountId = _loginService.accountId
  }

  openTransactionPartnerDetailsDialog(): void {
    const RBdialogRef = this._dialog.open(DialogTransactionPartnerDetailsComponent, {
      width: '50%',
      height: '80%',
      data: {
        transactionPartner: this.transactionPartner
      },
    });

    RBdialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  ngOnInit(): void {
    this.loadOperations();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  // ngAfterViewInit() {;
  //   merge(this.paginator.page)
  //     .pipe(
  //       startWith({}),
  //       switchMap(() => {
  //         this.isLoadingResults = true;
  //         return this._operationsHistoryService.getOperationsHistory(
  //           this.accountId!,
  //           // ובסרביס גם כסטרינגthis.sort.active,
  //           //this.sort.direction,
  //           this.paginator.pageIndex,
  //           this.numOfRecords
  //         ).pipe(catchError(() => observableOf(null)));
  //       }),
  //       map(data => {
  //         // Flip flag to show that loading has finished.
  //         this.isLoadingResults = false;
  //         this.isRateLimitReached = data === null;
  //         if (data === null) {
  //           return [];
  //         }
  //         this.resultsLength = data.length;
  //         return data;
  //       }),
  //     )
  //     .subscribe(data => (this.data = data));
  // }
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
      this.resultsLength = result.length;
      //updates???
      this.paginator.pageIndex = this.currentPage;
    }, err => {
      this.isLoadingResults = false;
    })
  }
  pageChanged(event: PageEvent) {
    //this.currentPage = event.pageIndex;
    this.loadOperations();
  }

  viewTransactionPartnerDetails(transactionPartnerId: string) {
    this.transactionPartner = this._operationsHistoryService.getTransactionParnerByAccountId(transactionPartnerId);
    if (!this.transactionPartner) {
      this._operationsHistoryService.getTransactionPartnerDetails(transactionPartnerId).subscribe(res => {
        this.transactionPartner = res;
        this._operationsHistoryService.addTransactionParner(res);
        this.openTransactionPartnerDetailsDialog();
      })
    }
    else
      this.openTransactionPartnerDetailsDialog();
  }
}
