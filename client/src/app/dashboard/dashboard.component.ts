import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Customer } from '../_models/Customer';
import { CustomerService } from '../_services/customer.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
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
  