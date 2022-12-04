import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();

  data: any = {}

  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

 

  register() {
    this.accountService.register(this.data).subscribe({
      next:() => {this.router.navigateByUrl('/productsList');
    }, 
    error: () => {
    }}
    )
  }

  cancel() {
    this.router.navigateByUrl('/');
    this.cancelRegister.emit(false);
  }


}
