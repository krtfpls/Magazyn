import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { QuantityModalComponent } from 'src/app/modals/quantity-modal/quantity-modal.component';
import { DocumentLine } from 'src/app/_models/DocumentEntity';
import { DocumentLineHandle } from 'src/app/_models/DocumentLineHandle';
import { Product } from 'src/app/_models/product';


@Component({
  selector: 'app-document-create-lines',
  templateUrl: './document-create-lines.component.html',
  styleUrls: ['./document-create-lines.component.css']
})

export class DocumentCreateLinesComponent implements OnInit { bsModalRef?: BsModalRef;
  displayProductListMode: boolean = false;
  displayNewProductMode: boolean = false;
  documentLinesHandle: DocumentLineHandle;

  @Input() lines: DocumentLine[] = {} as DocumentLine[]; 
  @Output() listDone= new EventEmitter<DocumentLine[]>();

  constructor(private modalService: BsModalService) { 
    if (this.lines.length > 0)
    this.documentLinesHandle = new DocumentLineHandle(this.lines);
    else
    this.documentLinesHandle = new DocumentLineHandle();
  }

  ngOnInit(): void { 
   }

   listDoneEvent(){
    this.listDone.emit(this.documentLines);
   }

  get total(){
    return this.documentLinesHandle.total;
  }

    get documentLines(){
    return this.documentLinesHandle.documentLines;
  }


  displayNewProductModeChange(){
    this.displayProductListMode=false;
    this.displayNewProductMode= !this.displayNewProductMode;
  }

  displayProductListModeChange() {
    this.displayNewProductMode = false,
    this.displayProductListMode = !this.displayProductListMode;
  }

  openQtyModal(product: Product) {
      this.bsModalRef = this.modalService.show(QuantityModalComponent, this.modalConfig(product));
      this.bsModalRef.content.event.subscribe((res: any) => {
         this.documentLinesHandle.addProduct(product, parseInt(res.qty));
      });

    this.displayNewProductMode=false;
    this.displayProductListMode=false;
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

    clearDocumentLines(){
    this.documentLinesHandle.clearAll();
  }
}
