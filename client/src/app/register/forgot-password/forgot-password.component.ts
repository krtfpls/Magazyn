import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Email } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  forgotForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  @Output() cancelForgot = new EventEmitter<boolean>();
  
  constructor(private accountService: AccountService, private fb: FormBuilder,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  sendForgotLink(){
    const email:Email = this.forgotForm.value;
  
    this.accountService.forgotPassword(email).subscribe({
      next: () =>{
          this.toastr.success("Link do zmiany hasła został pomyślne przesłany, Sprawdź swoją skrzynkę odbiorczą"); 
          window.location.reload();
      }
    });
  }

  cancel(){
    this.cancelForgot.emit(false);
  }

  private initializeForm(){
    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      token: ['']
    });
  }
}
