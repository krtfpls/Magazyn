import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Category } from 'src/app/_models/category';

@Component({
  selector: 'app-category-modal',
  templateUrl: './category-modal.component.html',
  styleUrls: ['./category-modal.component.css']
})
export class CategoryModalComponent implements OnInit {
  @Output() ChosenCategoryEvent: EventEmitter<Category> = new EventEmitter<Category>();
  
  title?: string = "Przewiń aby załadować więcej";
  closeBtnName?: string = "Anuluj";
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  categoryChosen(item: Category){
    this.ChosenCategoryEvent.emit(item);
    this.cancel();
  }

  cancel(){
    this.bsModalRef.hide();
  }
}
