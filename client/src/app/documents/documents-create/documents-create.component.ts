import { Component, HostListener, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { QuantityModalComponent } from 'src/app/modals/quantity-modal/quantity-modal.component';
import { DocumentEntity, DocumentLine } from 'src/app/_models/DocumentEntity';
import { Product } from 'src/app/_models/product';
import { DocumentsService } from 'src/app/_services/documents.service';

@Component({
  selector: 'app-documents-create',
  templateUrl: './documents-create.component.html',
  styleUrls: ['./documents-create.component.css']
})

export class DocumentsCreateComponent implements OnInit {
  bsModalRef?: BsModalRef;
  documentEntity: DocumentEntity | undefined;
  displayProductList: boolean = false;

  // @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any) {
  //   if (this.documentLines.length > 0) {
  //     $event.returnValue = true;
  //   }
  // }

  constructor(private documentService: DocumentsService, private modalService: BsModalService) { }

  ngOnInit(): void {

  }

  get documentLines(){
    return this.documentService.documentLinesHandle.documentLines;
  }

  get total(){
    return this.documentService.documentLinesHandle.total;
  }

  openQtyModal(product: Product, change?: boolean) {

    if (product.serialNumber.length > 0) {
      this.documentService.documentLinesHandle.addProduct(product, 1)
    }
    else {
      this.bsModalRef = this.modalService.show(QuantityModalComponent, this.modalConfig(product));
      this.bsModalRef.content.event.subscribe((res: any) => {
        this.documentService.documentLinesHandle.addProduct(product, parseInt(res.qty));
      });
    }
    if (!change)
    this.productListModeChange();
  }

  productListModeChange() {
    this.displayProductList = !this.displayProductList;
  }

  qtyStepUp(itemId: string){
    this.documentService.documentLinesHandle.stepUpQty(itemId);
  }

  qtyStepDown(itemId: string){
    this.documentService.documentLinesHandle.stepDownQty(itemId);
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
