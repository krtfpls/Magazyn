import { Component,  EventEmitter,  Input,  OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { plLocale } from 'ngx-bootstrap/locale';
import { Customer } from 'src/app/_models/Customer';
import { DocumentEntity, DocumentLine } from 'src/app/_models/DocumentEntity';
import { DocumentType } from 'src/app/_models/DocumentType';
import { DocumentsService } from 'src/app/_services/documents.service';
defineLocale('pl', plLocale);

@Component({
  selector: 'app-document-create-header',
  templateUrl: './document-create-header.component.html',
  styleUrls: ['./document-create-header.component.css']
})
export class DocumentCreateHeaderComponent implements OnInit {
date: Date = new Date();
@Input() type: DocumentType | undefined;
@Input() documentLines: DocumentLine[]= {} as DocumentLine[];
number: string= '##/####';
checkData: boolean = false;
bsConfig: Partial<BsDatepickerConfig> | undefined;
customer: Customer | undefined;
documentNew: DocumentEntity = {} as DocumentEntity;
displayCustomerMode: boolean = false;
@Output() listDone= new EventEmitter();

constructor(private localeService: BsLocaleService, private documentService: DocumentsService, private router: Router) {
  this.setDatePickerConfig();
  }

  ngOnInit(): void {
   this.checkData= this.chekLines();
  }

  setCustomer(item: Customer){
    this.customer= item;
    this.checkData=false;
    this.displayCustomerModeChange();
  }

  listDoneEvent(){
    this.listDone.emit();
  }

  sendDocument(){
    if (this.customer && this.documentLines && this.type && this.date){
    this.documentNew.customer = this.customer;
    this.documentNew.date= this.formatDate(this.date);
    this.documentNew.documentLines= this.documentLines;
    this.documentNew.number= this.number;
    this.documentNew.type= this.type;

    this.documentService.sendNewDocument(this.documentNew, this.type).subscribe({
      next: (id:any) => {
        //this.documentService.clearDocumentLines();
        this.router.navigateByUrl('documentsDetail/'+id);
      }
    });
   
  }
  }
  private formatDate(date: Date){
    return date.toISOString().split('T')[0];
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

  private chekLines() {
    if (this.documentLines.length > 1)
      {
        this.documentNew?.documentLines=== this.documentLines;
      return false
    }
    else
      return true;
  }
}


