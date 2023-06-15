import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-verify-products',
  templateUrl: './verify-products.component.html',
  styleUrls: ['./verify-products.component.css']
})
export class VerifyProductsComponent implements OnInit {
  @Input() productsToVerify: string[] = [];
  products: Product[] = [];

  constructor() {}

  ngOnInit(): void {
    this.setProductsList();
  }

  setProductsList(): void{
    for (const tempRow of this.productsToVerify){
      const tempProduct: Product = {
        id: "",
        name: tempRow[0],
        serialNumber: "",
        priceNetto: this.setPrice(tempRow[4]),
        minLimit: 0,
        quantity: parseInt(tempRow[2],10),
        description: tempRow[6],
        categoryName: "migracja"
      }
      this.products.push(tempProduct);
    }
}

setPrice(price: string){
  if (price.length > 0)
    price.trim();
  
    return parseFloat(price);
}

}
