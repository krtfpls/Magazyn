import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AlertModule } from 'ngx-bootstrap/alert';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule.forRoot({
      type: 'square-jelly-box'
    }),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    AlertModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    NgxSpinnerModule,
    PaginationModule,
    ModalModule,
    AlertModule
  ]
})
export class SharedModule { }
