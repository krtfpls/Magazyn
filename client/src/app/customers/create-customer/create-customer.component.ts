import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Customer } from 'src/app/_models/Customer';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.css']
})
export class CreateCustomerComponent implements OnInit {
@Output() setCustomerEvent = new EventEmitter();
validationErrors: string[] | undefined;
customerForm: FormGroup = new FormGroup({});

  constructor(private customerService: CustomerService, private toastr: ToastrService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  createCustomer(item: Customer){
    this.customerService.createCustomer(item).subscribe({
      next: (id: any) => {
        item.id= id;
        this.setCustomerEvent.emit(item);
        this.toastr.success('Dostawca utworzony poprawnie');
      },
      error: error => {
        this.validationErrors = error;
      }
    })
  }

  private initializeForm() {
    this.customerForm = this.fb.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      street: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      streetName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      city: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      taxNumber: ['', [Validators.minLength(3), Validators.maxLength(100)]],
      description: ['', [Validators.minLength(3), Validators.maxLength(500)]],
    })
  }


}
