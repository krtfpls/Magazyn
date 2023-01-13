import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DocumentsService } from 'src/app/_services/documents.service';
import { environment } from 'src/environments/environment';
import { DocumentEntity } from '../../_models/DocumentEntity';

@Component({
  selector: 'app-documents',
  templateUrl: './documents-list.component.html',
  styleUrls: ['./documents-list.component.css']
})
export class DocumentsListComponent implements OnInit {
documents: DocumentEntity[] = [];
baseUrl = environment.apiUrl+'documents';

  constructor(private documentService: DocumentsService) { }

  ngOnInit(): void {
    this.getAllDocuments();
  }

  getAllDocuments(){
    this.documentService.getAllDocuments().subscribe({
      next: docs => this.documents=docs
    })
  }
}
