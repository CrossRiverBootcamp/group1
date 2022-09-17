import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, SortDirection } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { OperationsHistoryService } from '../services/operations-history.service';
import { OperationData } from 'src/app/models/operation-data';
import { TransactionPartner } from 'src/app/models/transaction-partner';
import { MatDialog } from '@angular/material/dialog';
import { DialogTransactionPartnerDetailsComponent } from '../dialog-transaction-partner-details/dialog-transaction-partner-details.component';
import { LoginService } from '../services/login.service';

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

  resultsLength: number = 0;
  isLoadingResults: boolean = true;
  isRateLimitReached: boolean = false;

  numOfRecords: number = 5;
  accountId :string;
  //accountId = sessionStorage.getItem('accountId');
  transactionPartner!:TransactionPartner;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private _operationsHistoryService: OperationsHistoryService,private _loginService:LoginService, private _dialog: MatDialog,
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
  }

  ngAfterViewInit() {
    //this.exampleDatabase = new ExampleHttpDatabase(this._httpClient);

    //If the user changes the sort order, reset back to the first page.
   // this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._operationsHistoryService.getOperationsHistory(
            this.accountId!,
            // ובסרביס גם כסטרינגthis.sort.active,
            //this.sort.direction,
            this.paginator.pageIndex,
            this.numOfRecords
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data === null;

          if (data === null) {
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = data.length;
          return data;
        }),
      )
  }

  viewTransactionPartnerDetails(accountId:string){
    this._operationsHistoryService.getTransactionPartnerDetails(accountId).subscribe(res=>{
      this.transactionPartner=res;
      this.openTransactionPartnerDetailsDialog();
    })
  }
}

