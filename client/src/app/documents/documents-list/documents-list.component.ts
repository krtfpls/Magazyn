import { Component, OnInit } from '@angular/core';
import { DocumentParams } from 'src/app/_models/DocumentParams';
import { Pagination } from 'src/app/_models/pagination';
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
  documentParams: DocumentParams | undefined;
  pagination: Pagination | undefined;
  baseUrl = environment.apiUrl + 'documents';

  constructor(private documentService: DocumentsService) { }

  ngOnInit(): void {
    this.documentParams = new DocumentParams();
    this.getDocuments();
  }

  getDocuments() {
    if (this.documentParams) {

      this.documentService.getAllDocuments(this.documentParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.documents = response.result,
              this.pagination = response.pagination}
        }
      })
    }
  }

  resetFilters() {
    this.documentParams = this.resetParams();
    this.getDocuments();
  }

  pageChanged(event: any) {
    if (this.documentParams && this.documentParams?.pageNumber !== event.page) {
      this.documentParams.pageNumber = event.page;
      this.getDocuments();
    }
  }

  resetParams() {
    this.documentParams = new DocumentParams();
    return this.documentParams;
  }

  getParams() {
    return this.documentParams;
  }

  setParams(params: DocumentParams) {
    this.documentParams = params;
  }
}
