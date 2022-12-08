import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DocumentDto } from '../_models/DocumentDto';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {
documents: DocumentDto[] = [];
baseUrl = environment.apiUrl+'documents';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllDocuments();
  }

  getAllDocuments(){
    this.http.get<DocumentDto[]>(this.baseUrl).subscribe({
      next: docs => this.documents=docs
    })
  }
}
