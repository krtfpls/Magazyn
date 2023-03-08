import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-products-list-all',
  templateUrl: './products-list-all.component.html',
  styleUrls: ['./products-list-all.component.css']
})
export class ProductsListAllComponent implements OnInit {
showAll:boolean = true;
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  editProductMode(product: Product){
    this.router.navigateByUrl('/productsCreate/'+ product.id);
  }
}
