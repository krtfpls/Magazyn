import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../_models/category';
import { PaginatedResult } from '../_models/pagination';
import { Product } from '../_models/product';
import { ProductParams } from '../_models/ProductParams';
import {getPaginatedResult, getPaginationHeaders} from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  products: Product[] | undefined = [];
  productsCache = new Map();
  product: Product | undefined;
  baseUrl = environment.apiUrl + 'products/';
  categoryUrl = environment.apiUrl + 'category/';
  productParams: ProductParams | undefined;
  
  constructor(private http: HttpClient) {
    this.productParams = new ProductParams();
   }

   getCategories(){
    return this.http.get<Category[]>(this.categoryUrl);
    
   }

  getAllProducts(productParams: ProductParams){
    const response = this.productsCache.get(Object.values(productParams).join('-'));
    if (response) return of(response);

    let params = getPaginationHeaders(productParams.pageNumber, productParams.pageSize);
    params = params.append('CategoryName', productParams.CategoryName);
    
    return getPaginatedResult<Product[]>(this.baseUrl, params, this.http).pipe(
      map (response => {
        this.addToMapObject(productParams, response);
        return response;
      })
    );
    
  }

  getProductDetail(id: string | undefined) {
    if (id) {
      return this.http.get<Product>(this.baseUrl + id);
    }
    else
    return undefined
  }

  createProduct(product: Product){
    return this.http.post<Product>(this.baseUrl, product).pipe(
      map( _ => {
        this.productsCache.clear();
      })
    );
  }

  updateProduct(product: Product){
    return this.http.put(this.baseUrl,product).pipe(
      map(() => {
        if (this.products){
        const index = this.products.indexOf(product);
        this.products[index] = {...this.products[index], ...product};
        }
      })
    )
  }

  addToMapObject(params: ProductParams, data: PaginatedResult<Product[]>){
    if (this.productsCache.size >= 30)
      this.productsCache.clear();
    this.productsCache.set(Object.values(params).join('-'), data);
  }

  resetProductParams() {
      this.productParams= new ProductParams();
      return this.productParams;
  }

  getProductParams() {
    return this.productParams;
  }

  setProductParams(params: ProductParams){
    this.productParams = params;
  }

}
