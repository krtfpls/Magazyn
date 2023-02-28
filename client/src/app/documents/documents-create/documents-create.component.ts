import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DocumentLine } from 'src/app/_models/DocumentEntity';
import { DocumentLineHandle } from 'src/app/_models/DocumentLineHandle';
import { DocumentType } from 'src/app/_models/DocumentType';
import { DocumentsService } from 'src/app/_services/documents.service';

@Component({
  selector: 'app-documents-create',
  templateUrl: './documents-create.component.html',
  styleUrls: ['./documents-create.component.css']
})

export class DocumentsCreateComponent implements OnInit {
  bsModalRef?: BsModalRef;
  displayCustomerMode: boolean  = false;
  displayLinesMode: boolean = true;
  type: DocumentType | undefined;
  documentLines: DocumentLine[]= {} as DocumentLine[];
  
  constructor(private documentService: DocumentsService, private activatedRoute: ActivatedRoute) {
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

  // get documentLines(){
  //   return this.documentLinesHandle.documentLines;
  // }

  // addLine(item: DocumentLine){
  //   this.documentLinesHandle.addProduct(item.product, item.quantity);
  // }

  // clearDocumentLines(){
  //   this.documentLinesHandle.clearAll();
  // }

}
