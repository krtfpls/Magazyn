import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DocumentEntity } from 'src/app/_models/DocumentEntity';
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
    this.getDocument(this.id);
  }
  
  getDocument(id: string | undefined) {
      this.route.params.subscribe(params => this.id = params['id']);
      if (this.id) {
        this.documentService.getDocumentDetails(this.id)?.subscribe({
          next: response => this.documentEntity = response,
          error: error => console.log(error)
        });
      }
  }


}
