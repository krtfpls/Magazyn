import { Component, Input, OnInit } from '@angular/core';
import { DocumentLine } from 'src/app/_models/DocumentEntity';

@Component({
  selector: 'app-document-lines',
  templateUrl: './document-lines.component.html',
  styleUrls: ['./document-lines.component.css']
})
export class DocumentLinesComponent implements OnInit {
  @Input() documentLines: DocumentLine[] | undefined;
  
  constructor() { }

  ngOnInit(): void {
  }

}
