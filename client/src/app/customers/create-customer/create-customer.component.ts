import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.css']
})
export class CreateCustomerComponent implements OnInit {
@Output() setCustomerEvent = new EventEmitter();
  
  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
  }

}
