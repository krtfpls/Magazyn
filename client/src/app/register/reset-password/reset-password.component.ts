import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { passwordResetModel } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
passwordResetForm: FormGroup = new FormGroup({});
validationErrors: string[] | undefined; 
token: string ='';
email: string = ''; 

constructor(private accountService: AccountService, private fb: FormBuilder, private route: ActivatedRoute,
      private toastr: ToastrService, private router: Router) { }
  
    ngOnInit(): void {
      this.route.queryParams.subscribe(params => 
        {
        this.token = params['token'];
        this.email = params['email'];
        })
      if (this.token && this.email){
    };

    this.initializeForm();
  }
  
    resetPassword(){
      if (this.passwordResetForm.valid){
      const password: passwordResetModel = this.passwordResetForm.value;
      this.accountService.resetPassword(password).subscribe({
        next: response => {
          this.passwordResetForm.reset();
          this.toastr.success(response);
          this.cancel();
        }
      });
    }
    }
  
    private initializeForm(){
      this.passwordResetForm = this.fb.group({
        password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]],
        confirmPassword: ['', [Validators.required, this.matchValues('password')]],
        email: [this.email, [Validators.required, Validators.email, Validators.maxLength(100)]],
        token: [this.token, [Validators.required]],
      });
  
      this.passwordResetForm.controls['password'].valueChanges.subscribe({
        next: () => this.passwordResetForm.controls['confirmPassword'].updateValueAndValidity()
      })
    }
  
    private matchValues(matchTo: string): ValidatorFn {
      return (control: AbstractControl) => {
        return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
      }
    }
  
    cancel(){
      this.router.navigateByUrl("/");
    }
  }