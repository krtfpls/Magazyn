import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})

export class ProductsListComponent implements OnInit {
products: Product[] = []
  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts()
  }
  loadProducts() {
    this.productService.getAllProducts().subscribe({
      next: response => {
        this.products= response;
      }
    })
  }


}
