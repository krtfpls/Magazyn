import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { ProductCreateComponent } from '../products/product-create/product-create.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<ProductCreateComponent> {
  constructor(private confirmService: ConfirmService){}

  canDeactivate(component: ProductCreateComponent): Observable<boolean> {
    if (component.productForm?.dirty) {
      return this.confirmService.confirm()
    }

    return of(true);
  }

  
}
