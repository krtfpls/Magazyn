import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Customer } from 'src/app/_models/Customer';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-list-customer',
  templateUrl: './list-customer.component.html',
  styleUrls: ['./list-customer.component.css']
})
export class ListCustomerComponent implements OnInit {
customers: Customer[] = [];
displayNewCustomerMode: boolean= false;
@Output() event = new EventEmitter<Customer>();

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.getCustomer();
  }

  getCustomer(){
    this.customerService.getCustomers().subscribe({
      next: response => {
        this.customers = response;
      }
    })
  }

  setDisplayNewCustomerMode(){
    this.displayNewCustomerMode= !this.displayNewCustomerMode;
  }

  setCustomer(item: Customer){
    this.event.emit(item);
  }

}
