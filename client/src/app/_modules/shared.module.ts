import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AlertModule } from 'ngx-bootstrap/alert';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxNavbarModule } from 'ngx-bootstrap-navbar';
import { InfiniteScrollModule } from "ngx-infinite-scroll";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule.forRoot({
      type: 'square-jelly-box'
    }),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    AlertModule.forRoot(),
    NgxNavbarModule,
    InfiniteScrollModule
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    NgxSpinnerModule,
    PaginationModule,
    ModalModule,
    AlertModule,
    BsDatepickerModule,
    NgxNavbarModule,
    InfiniteScrollModule
  ]
})
export class SharedModule { }
