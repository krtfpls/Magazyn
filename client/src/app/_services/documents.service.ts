import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DocumentDto } from '../_models/DocumentDto';

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  documents: DocumentDto[] = [];
  baseUrl = environment.apiUrl + 'products/';
  constructor() { }
}
