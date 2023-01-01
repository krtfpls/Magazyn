import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-products-detail',
  templateUrl: './products-detail.component.html',
  styleUrls: ['./products-detail.component.css']
})
export class ProductsDetailComponent implements OnInit {
id: string | undefined;  
product: Product | undefined;

constructor(private productService: ProductService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params =>
      this.id= params['id']);
  
    this.productService.getProductDetail(this.id)?.subscribe({
      next: response => this.product=response,
      error: error => console.log(error)
    });
  }

  // ngOnDestroy(): void{
  //   this.id= undefined;
  //   this.product= undefined;
  // }
}
