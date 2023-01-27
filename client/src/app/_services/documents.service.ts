import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DocumentEntity } from '../_models/DocumentEntity';
import { DocumentLineHandle } from '../_models/DocumentLineHandle';
import { DocumentParams } from '../_models/DocumentParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  baseUrl = environment.apiUrl + 'documents/';
  documentLinesHandle: DocumentLineHandle= new DocumentLineHandle();
  
  constructor(private http: HttpClient) { }

  getAllDocuments(documentParams: DocumentParams) {
    let params = getPaginationHeaders(documentParams.pageNumber, documentParams.pageSize);
    return getPaginatedResult<DocumentEntity[]>(this.baseUrl, params, this.http);
 
  }

  getDocumentDetails(id: string) {
    return this.http.get<DocumentEntity>(this.baseUrl + id);
  }

}
