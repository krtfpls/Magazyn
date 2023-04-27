import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AlertModule } from 'ngx-bootstrap/alert';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { InfiniteScrollModule } from "ngx-infinite-scroll";
import { CollapseModule } from 'ngx-bootstrap/collapse';

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
    InfiniteScrollModule,
    CollapseModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    NgxSpinnerModule,
    PaginationModule,
    ModalModule,
    AlertModule,
    BsDatepickerModule,
    InfiniteScrollModule,
    CollapseModule
  ]
})
export class SharedModule { }
