import { Component, EventEmitter,  OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-quantity-modal',
  templateUrl: './quantity-modal.component.html',
  styleUrls: ['./quantity-modal.component.css']
})

export class QuantityModalComponent implements OnInit {
  qtyForm: FormGroup = new FormGroup({});
  title?: string= "Podaj ilość";
  closeBtnName?: string = "Anuluj";
  product: Product | undefined;
  @Output() event: EventEmitter<number> = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  qtyRemove(){
    const qty = parseInt(this.qtyForm.get('qty')?.value)  as number -1;
    this.qtyForm.get('qty')!.setValue(qty);
  }

  qtyAdd(){
    const qty = parseInt(this.qtyForm.get('qty')?.value)  as number +1;
    this.qtyForm.get('qty')!.setValue(qty);
  }

  addToList(form: FormGroup) {
    if (form.value){
      const qty: number= form.value as number;
      this.triggerEvent(qty);
      this.bsModalRef.hide();
    }
  }

  cancel() {
    this.bsModalRef?.hide()
  }

  private initializeForm(){
    this.qtyForm = this.formBuilder.group({
      qty: [1, [Validators.required, Validators.min(-9999), Validators.max(9999)]]
    })
  }

  private triggerEvent(line: number) {
    this.event.emit(line);
  }

}
