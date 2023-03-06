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
  