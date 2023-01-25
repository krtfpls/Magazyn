import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { QuantityModalComponent } from 'src/app/modals/quantity-modal/quantity-modal.component';
import { DocumentEntity } from 'src/app/_models/DocumentEntity';
import { DocumentLineHandle } from 'src/app/_models/DocumentLineHandle';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-documents-create',
  templateUrl: './documents-create.component.html',
  styleUrls: ['./documents-create.component.css']
})
export class DocumentsCreateComponent implements OnInit, OnDestroy {
  bsModalRef?: BsModalRef;
  documentEntity: DocumentEntity | undefined;
  documentLinesHandle: DocumentLineHandle= new DocumentLineHandle();
  displayProductList: boolean = false;

  constructor(private productService: ProductService, private modalService: BsModalService) {

  }

  ngOnDestroy(): void {
    this.productService.clearProductsCache();
  }

  ngOnInit(): void {
  }

  openQtyModal(product: Product) {

    if (product.serialNumber.length > 0) {
      this.documentLinesHandle.addProduct(product, 1)
    }
    else {
      this.bsModalRef = this.modalService.show(QuantityModalComponent, this.modalConfig(product));
      this.bsModalRef.content.closeBtnName = 'Anuluj';
      this.bsModalRef.content.event.subscribe((res: any) => {
        this.documentLinesHandle.addProduct(product, parseInt(res.qty));
      });
    }
    this.productListModeChange();
  }

  productListModeChange() {
    this.displayProductList = !this.displayProductList;
  }

  private modalConfig(initial: Product): ModalOptions{

    const initialState: ModalOptions = {
      initialState: {
        product: initial
      },
      class: 'modal-dialog-centered',
      backdrop: true,
      ignoreBackdropClick: true
    };
    return initialState;
  }
}
