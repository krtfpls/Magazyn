import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Customer } from '../_models/Customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseUrl = environment.apiUrl + 'customer/';
  
  constructor(private http: HttpClient) { }

  getCustomers(){
    return this.http.get<Customer[]>(this.baseUrl);
  }

  createCustomer(customer: Customer){
    return this.http.post<Customer>(this.baseUrl, customer);
  }
}
