import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})

export class ProductsListComponent implements OnInit {

constructor(private router: Router) {}

  ngOnInit(): void { }

  editProductMode(product: Product){
      this.router.navigateByUrl('/productsCreate/'+ product.id);
    }

}
