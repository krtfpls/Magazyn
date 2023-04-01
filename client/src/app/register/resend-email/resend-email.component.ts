import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Email } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-resend-email',
  templateUrl: './resend-email.component.html',
  styleUrls: ['./resend-email.component.css']
})
export class ResendEmailComponent implements OnInit {
  reSendForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  @Output() cancelReSend = new EventEmitter<boolean>();
  
  constructor(private accountService: AccountService, private fb: FormBuilder,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  public resend(){
    const email:Email = this.reSendForm.value;
  
    this.accountService.resendEmail(email).subscribe({
      next: () => {
        this.toastr.success("Email przesłany ponownie");
      }
    });
  }

  cancel(){
    this.cancelReSend.emit(false);
  }

  private initializeForm(){
    this.reSendForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      token: ['']
    });
  }
}
