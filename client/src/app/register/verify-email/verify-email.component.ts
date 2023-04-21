import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.css']
})
export class VerifyEmailComponent implements OnInit {
token: string ='';
email: string = '';

  constructor(private accountService: AccountService, private route: ActivatedRoute,
       private router: Router, private toastr: ToastrService) { 

       }

  ngOnInit(): void {
          this.route.queryParams.subscribe(params => 
          {
          this.token = params['token'];
          this.email = params['email'];
          })
        if (this.token && this.email){
        this.sendToVerify(this.token, this.email);
        }
  }

  sendToVerify(token: string, email: string) {
    this.accountService.verifyEmail(token, email).subscribe({
      next: _ => 
       { 
        this.toastr.success('Email zweryfikowany');
        this.router.navigateByUrl('/documentsList')
      },
      error: error => console.log(error)
    });;
  }

}



