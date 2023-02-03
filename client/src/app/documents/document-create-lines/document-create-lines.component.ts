import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { QuantityModalComponent } from 'src/app/modals/quantity-modal/quantity-modal.component';
import { DocumentLine } from 'src/app/_models/DocumentEntity';
import { Product } from 'src/app/_models/product';
import { DocumentsService } from 'src/app/_services/documents.service';


@Component({
  selector: 'app-document-create-lines',
  templateUrl: './document-create-lines.component.html',
  styleUrls: ['./document-create-lines.component.css']
})

export class DocumentCreateLinesComponent implements OnInit { bsModalRef?: BsModalRef;
  displayProductListMode: boolean = false;
  displayNewProductMode: boolean = false;
  @Input() documentLines: DocumentLine[]= {} as DocumentLine[];
  @Output() addLineEvent = new EventEmitter<DocumentLine>();
  @Output() listDone= new EventEmitter<boolean>();

  constructor(private documentService: DocumentsService, private modalService: BsModalService) { }

  ngOnInit(): void { 
   }

   listDoneEvent(){
    this.listDone.emit();
   }

  get total(){
    return this.documentLines.total;
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
        this.documentService.documentLinesHandle.addProduct(product, parseInt(res.qty));
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

 
}
