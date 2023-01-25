import { Component, Input, OnInit, Output, EventEmitter, OnDestroy } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { Product } from 'src/app/_models/product';
import { ProductParams } from 'src/app/_models/ProductParams';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.css']
})
export class ProductsTableComponent implements OnInit {
@Input() actionColumnCaption: string | undefined;
@Input() iconAwesome?: string = "fa fa-edit";
@Input() showAll? : boolean = false;

@Output() buttonAction = new EventEmitter();

products: Product[] = [];
pagination: Pagination | undefined;
productParams: ProductParams | undefined;
smallScreen: boolean = false;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productParams= this.productService.getParams();
    this.loadProducts()
    if (window.screen.width > 390) {
      this.smallScreen = true;
    }
  }


  clickButton(product: Product) {
    this.buttonAction.emit(product);
  }

  loadProducts() {
    if (this.productParams){
      this.productService.setParams(this.productParams);

      const getProducts = (this.showAll === true) ? (this.productService.getAllProducts(this.productParams)) :
        (this.productService.getStockProducts(this.productParams));

      getProducts.subscribe({
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
