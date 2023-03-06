import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Customer } from 'src/app/_models/Customer';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  customers: Customer[] = [];
  displayNewCustomerMode: boolean= false;
  @Output() event = new EventEmitter<Customer>();
  
    constructor() { }
  
    ngOnInit(): void {
    }
  
    setDisplayNewCustomerMode(){
      this.displayNewCustomerMode= !this.displayNewCustomerMode;
    }
  
    setCustomer(item: Customer){
      this.event.emit(item);
    }
  
  }
  