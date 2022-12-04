import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  
  login() {
    this.accountService.login(this.model).subscribe({
      next:() => {
        this.router.navigateByUrl('/productsList');
      },
      error: () => {
      }
    }
    )
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  cancel(){
    this.cancelRegister.emit(false);
  }

}
