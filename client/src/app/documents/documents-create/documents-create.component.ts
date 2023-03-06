import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Customer } from 'src/app/_models/Customer';
import { DocumentEntity, DocumentLine } from 'src/app/_models/DocumentEntity';
import { DocumentType } from 'src/app/_models/DocumentType';
import { DocumentsService } from 'src/app/_services/documents.service';

@Component({
  selector: 'app-documents-create',
  templateUrl: './documents-create.component.html',
  styleUrls: ['./documents-create.component.css']
})

export class DocumentsCreateComponent implements OnInit {
  bsModalRef?: BsModalRef;
  customer: Customer | undefined;
  number: string= '##/####';
  checkData: boolean = false;
  documentNew: DocumentEntity = {} as DocumentEntity;
  displayCustomerMode: boolean  = false;
  displayLinesMode: boolean = true;
  type: DocumentType | undefined;
  documentLines: DocumentLine[]= {} as DocumentLine[];
  
  constructor(private documentService: DocumentsService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(params => this.type = params['type']);
   }

  ngOnInit(): void { 
   
   }

   displayModeChange(){
    this.displayLinesMode=!this.displayLinesMode;
    this.displayCustomerMode = !this.displayCustomerMode;
  }

  setDocumentLines(items: DocumentLine[]){
    this.documentLines= items;
    this.displayModeChange();
  }

  setCustomer(item: Customer){
    this.customer= item;
  }

  sendDocument(date: string){
    if (this.customer && this.documentLines && this.type && date){
    this.documentNew.customer = this.customer;
    this.documentNew.date= date;
    this.documentNew.documentLines= this.documentLines;
    this.documentNew.number= this.number;
    this.documentNew.type= this.type;

    this.documentService.CreateDocument(this.documentNew, this.type).subscribe({
      next: (id:any) => {
        //this.documentService.clearDocumentLines();
        this.router.navigateByUrl('documentsDetail/'+id);
      }
    });
  }
  }

}
