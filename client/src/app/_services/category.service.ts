import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Category } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
categoryUrl = environment.apiUrl + 'category/';

  constructor(private http: HttpClient) { }

  getCategories(page= 1){
    return this.http.get<Category[]>(this.categoryUrl+'?pageNumber='+page);
   }
}
