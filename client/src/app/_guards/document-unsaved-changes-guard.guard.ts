import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { DocumentsCreateComponent } from '../documents/documents-create/documents-create.component';
import { ConfirmService } from '../_services/confirm.service';


@Injectable({
  providedIn: 'root'
})
export class DocumentUnsavedChangesGuardGuard implements CanDeactivate<DocumentsCreateComponent> {

  constructor (private confirmService: ConfirmService) {}

  canDeactivate(component: DocumentsCreateComponent): Observable<boolean> {
    if (component.documentLines.length > 0) {
      return this.confirmService.confirm()
    }

    return of(true);
  }

  
}
