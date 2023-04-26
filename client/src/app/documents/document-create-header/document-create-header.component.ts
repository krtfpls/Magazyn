import { Component,  EventEmitter,  Input,  OnInit, Output } from '@angular/core';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { plLocale } from 'ngx-bootstrap/locale';
import { Customer } from 'src/app/_models/Customer';
import { DocumentType } from 'src/app/_models/DocumentType';
defineLocale('pl', plLocale);

@Component({
  selector: 'app-document-create-header',
  templateUrl: './document-create-header.component.html',
  styleUrls: ['./document-create-header.component.css']
})
export class DocumentCreateHeaderComponent implements OnInit {
date: Date = new Date();
@Input() number: string= '';
@Input() type: DocumentType | undefined;
customer: Customer | undefined;
bsConfig: Partial<BsDatepickerConfig> | undefined;
displayCustomerMode: boolean = false;
@Output() backButtonEvent= new EventEmitter();
@Output() setCustomerEvent = new EventEmitter<Customer>();
@Output() headerDoneEvent = new EventEmitter<string>();

constructor(private localeService: BsLocaleService) {
  this.setDatePickerConfig();
  }

  ngOnInit(): void {

  }

  headerDone(){
    this.headerDoneEvent.emit(this.formatDate(this.date));
  }

  setCustomer(item: Customer){
    this.customer= item;
    this.setCustomerEvent.emit(item);
    this.displayCustomerModeChange();
  }

  validate(){
    if (this.date != null && this.customer)
      return false;
      else
      return true;
  }

  backButton(){
    this.backButtonEvent.emit();
  }

  displayCustomerModeChange() {
    this.displayCustomerMode= !this.displayCustomerMode;
  }

  private setDatePickerConfig() {
    this.localeService.use('pl');
    this.bsConfig = {
      containerClass: 'theme-default',
      dateInputFormat: 'DD.MM.YYYY',
      initCurrentTime: false,
      selectFromOtherMonth: true
    };
  }

  private formatDate(date: Date){
    return date.toISOString().split('T')[0];
   }
}


