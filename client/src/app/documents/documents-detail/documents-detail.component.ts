import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DocumentEntity } from 'src/app/_models/DocumentEntity';
import { Product } from 'src/app/_models/product';
import { DocumentsService } from 'src/app/_services/documents.service';

@Component({
  selector: 'app-documents-detail',
  templateUrl: './documents-detail.component.html',
  styleUrls: ['./documents-detail.component.css']
})
export class DocumentsDetailComponent implements OnInit {
  id: string | undefined;  
  documentEntity: DocumentEntity | undefined;

  constructor(private documentService: DocumentsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getDocument();
  }
  
  getDocument() {
      this.route.params.subscribe(params => this.id = params['id']);
      if (this.id) {
        this.documentService.getDocumentDetails(this.id)?.subscribe({
          next: response => {
            response.date = formatDate(response.date, 'dd.MM.yyyy', 'en-EN')
            this.documentEntity = response;
          },
          error: error => console.log(error)
        });
      }
  }


}
