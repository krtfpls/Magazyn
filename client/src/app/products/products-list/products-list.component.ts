import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Route } from '@angular/router';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  products= [];
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllProducts();
    
  }

  getAllProducts(){
   const data= this.http.get<any>(this.baseUrl+'products');
    data.subscribe({
      next: prods => this.products = prods
    })
    console.log(this.products);
  }
}
