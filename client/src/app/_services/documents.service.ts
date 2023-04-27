import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DocumentEntity, DocumentLineToSend, DocumentToSend } from '../_models/DocumentEntity';
import { DocumentParams } from '../_models/DocumentParams';
import { DocumentType } from '../_models/DocumentType';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  baseUrl = environment.apiUrl + 'documents/';
  
  constructor(private http: HttpClient) { }

  getAllDocuments(documentParams: DocumentParams) {
    let params = getPaginationHeaders(documentParams.pageNumber, documentParams.pageSize);
    return getPaginatedResult<DocumentEntity[]>(this.baseUrl, params, this.http);
 
  }

  getDocumentDetails(id: string) {
    return this.http.get<DocumentEntity>(this.baseUrl + id);
  }

  CreateDocument(inputDocument: DocumentEntity, docType: DocumentType){

     const doc= this.documentPrepare(inputDocument);

    switch (docType){
      case DocumentType.PZ:
        return this.http.post<DocumentEntity>(this.baseUrl+'GoodsReceiptNote', doc);
        break;
      case DocumentType.WZ:
        return this.http.post<DocumentEntity>(this.baseUrl+'GoodsDispatchNote', doc);
        break;
      default:
        return {} as Observable<DocumentEntity>;
        break;
    }
  }

  private documentPrepare(doc: DocumentEntity) {

    let docToSend: DocumentToSend = {} as DocumentToSend;
      docToSend.customerId = doc.customer!.id.toString();
      docToSend.date= doc.date;
      docToSend.number= doc.number;
      docToSend.type= doc.type;

      let lines: DocumentLineToSend[] = [];

      doc.documentLines.forEach((element) => {
       lines.push({ productId: element.product.id, quantity: element.quantity})
      });

      docToSend.documentLines=lines;
      return docToSend;
}
}