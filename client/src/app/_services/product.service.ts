import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  products:Product[]= [];
  baseUrl = environment.apiUrl+'products';
  
  constructor(private http: HttpClient) { }

  getAllProducts(){
    const response = this.products;
    if (response.length > 0 ) return of(response);

    return this.http.get<Product[]>(this.baseUrl).pipe(
      map((response: Product[]) => {
        this.products=response;
        return response;
      })
    )
   }
}
