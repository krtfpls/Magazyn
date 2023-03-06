import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from '../_models/Customer';
import { CustomerParams } from '../_models/CustomerParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  baseUrl = environment.apiUrl + 'customer/';

  constructor(private http: HttpClient) { }

  getCustomers(customerParams: CustomerParams){
    let params = getPaginationHeaders(customerParams.pageNumber, customerParams.pageSize);
       params = params.append('Name', customerParams.name);
    
    return getPaginatedResult<Customer[]>(this.baseUrl, params, this.http);
    }

  createCustomer(customer: Customer){
    return this.http.post<Customer>(this.baseUrl, customer);
  }

}
