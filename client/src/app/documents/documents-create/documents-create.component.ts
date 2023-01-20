import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DocumentEntity, DocumentLine } from 'src/app/_models/DocumentEntity';

@Component({
  selector: 'app-documents-create',
  templateUrl: './documents-create.component.html',
  styleUrls: ['./documents-create.component.css']
})
export class DocumentsCreateComponent implements OnInit {
  documentEntity: DocumentEntity | undefined;
  documentLines: DocumentLine | undefined;
  displayProductList: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  getProductList(){
    this.displayProductList= !this.displayProductList;
  }

}
