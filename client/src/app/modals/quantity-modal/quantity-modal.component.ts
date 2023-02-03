import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
  title?: string = "Podaj ilość";
  closeBtnName?: string = "Anuluj";
  product: Product | undefined;
  @Output() event: EventEmitter<number> = new EventEmitter();

  constructor(public bsModalRef: BsModalRef, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  qtyStepUp() {
    const qty = parseInt(this.qtyForm.get('qty')?.value) as number + 1;
    if (this.canStepUp(qty))
      this.qtyForm.get('qty')!.setValue(qty);
  }

  qtyStepDown() {
    const qty = parseInt(this.qtyForm.get('qty')?.value) as number - 1;
    if (this.canStepDown(qty))
      this.qtyForm.get('qty')!.setValue(qty);
  }

  canStepUp(qty: number) {
    if (this.product && this.product.serialNumber.length > 0) {
      if (qty > 1) 
        return false;
      else
        return true;
    }
    else {
      return true;
    }
  }

  canStepDown(qty: number) {
    if (this.product && this.product.serialNumber.length > 0) {
      if (qty < 0) 
        return false;
      else
        return true;
    }
    else {
      return true;
    }
  }

  add(form: FormGroup) {
    if (form.value) {
      const qty: number = form.value as number;
      this.triggerEvent(qty);
      this.bsModalRef.hide();
    }
  }

  remove(){
    this.qtyForm.get('qty')!.setValue(0);
    this.add(this.qtyForm);
  }

  cancel() {
    this.bsModalRef?.hide()
  }

  private initializeForm() {
    this.qtyForm = this.formBuilder.group({
      qty: [1, [Validators.required, Validators.min(-9999), Validators.max(9999)]]
    })
  }

  private triggerEvent(qty: number) {
    this.event.emit(qty);
  }

}
