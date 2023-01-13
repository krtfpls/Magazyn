import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { Product } from 'src/app/_models/product';
import { ProductParams } from 'src/app/_models/ProductParams';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})

export class ProductsListComponent implements OnInit {
products: Product[] = [];
pagination: Pagination | undefined;
productParams: ProductParams | undefined;

constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productParams= this.productService.getParams();
    this.loadProducts()
  }

  loadProducts() {
  
    if (this.productParams){

      this.productService.setParams(this.productParams);
      this.productService.getStockProducts(this.productParams).subscribe({
        next: response => {
          if (response.result && response.pagination){
            this.products= response.result;
            this.pagination= response.pagination;
          }
        }
      })
    } 
  }

  resetFilters() {
    this.productParams = this.productService.resetParams();
    this.loadProducts();
  }

  pageChanged(event: any) {
    if (this.productParams && this.productParams?.pageNumber !== event.page){
      this.productParams.pageNumber = event.page;
      this.productService.setParams(this.productParams);
      this.loadProducts();
    }
    }
}
