import { Component, OnInit } from '@angular/core';
import { DocumentDto } from 'src/app/_models/DocumentDto';
import { DocumentsService } from 'src/app/_services/documents.service';

@Component({
  selector: 'app-documents-detail',
  templateUrl: './documents-detail.component.html',
  styleUrls: ['./documents-detail.component.css']
})
export class DocumentsDetailComponent implements OnInit {
  id: string | undefined;  
  documentDto: DocumentDto | undefined;

  constructor(documentService: DocumentsService) { }

  ngOnInit(): void {
  }

}
