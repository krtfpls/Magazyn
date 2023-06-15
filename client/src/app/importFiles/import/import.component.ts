import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent implements OnInit {
  componentName: string= 'lomag';
  fileImportComponentEnabled: boolean = true;
  verifyDataComponentEnabled: boolean = false;
  productsToVerify: string[] = [];

  constructor() { }

  ngOnInit(): void {
  }

  verifyEvent(eventData: any){
    if (eventData){
    this.productsToVerify= eventData;
    this.fileImportComponentEnabled=false;
    this.verifyDataComponentEnabled=true;
    }
  }
}
