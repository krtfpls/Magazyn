import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Customer } from 'src/app/_models/Customer';
import { CustomerParams } from 'src/app/_models/CustomerParams';
import { Pagination } from 'src/app/_models/pagination';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-list-customer',
  templateUrl: './list-customer.component.html',
  styleUrls: ['./list-customer.component.css']
})
export class ListCustomerComponent implements OnInit {
customers: Customer[] = [];
displayNewCustomerMode: boolean= false;
pagination: Pagination | undefined;
customerParams: CustomerParams | undefined;
@Output() setCustomerEvent = new EventEmitter<Customer>();

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.customerParams= new CustomerParams();
    this.getCustomer();
  }

  getCustomer(){
    if (this.customerParams){
      
    this.customerService.getCustomers(this.customerParams).subscribe({
      next: response => {
        if (response.result && response.pagination){
          this.customers = response.result;
          this.pagination= response.pagination;
        }
      }
    })
  }}

  setCustomer(item: Customer){
    this.setCustomerEvent.emit(item);
  }

  resetFilters() {
    this.customerParams = this.resetParams();
    this.getCustomer();
  }

  pageChanged(event: any) {
    if (this.customerParams && this.customerParams?.pageNumber !== event.page) {
      this.customerParams.pageNumber = event.page;
      this.getCustomer();
    }
  }

  resetParams() {
    this.customerParams = new CustomerParams();
    return this.customerParams;
  }

  getParams() {
    return this.customerParams;
  }

  setParams(params: CustomerParams) {
    this.customerParams = params;
  }

}
