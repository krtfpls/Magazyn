import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DocumentEntity } from '../_models/DocumentEntity';
@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  documents: DocumentEntity[] = [];
  baseUrl = environment.apiUrl + 'documents/';

  constructor(private http: HttpClient) {}

  getAllDocuments(){
    return this.http.get<DocumentEntity[]>(this.baseUrl);
  }

  getDocumentDetails(id: string) {
    return this.http.get<DocumentEntity>(this.baseUrl + id);
  }
}
