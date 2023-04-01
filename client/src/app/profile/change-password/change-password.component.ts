import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { passwordChangeModel } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
passwordChangeForm: FormGroup = new FormGroup({});
validationErrors: string[] | undefined;
@Output() cancelResetEvent = new EventEmitter<boolean>();

  constructor(private accountService: AccountService, private fb: FormBuilder,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  changePassword(){
  
        const password: passwordChangeModel = this.passwordChangeForm.value;
    this.accountService.changePassword(password).subscribe({
      next: response => {
        this.passwordChangeForm.reset();
        this.toastr.success(response);
        this.cancel();
      }
    });
    
  }

  private initializeForm(){
    this.passwordChangeForm = this.fb.group({
      oldPassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    });

    this.passwordChangeForm.controls['password'].valueChanges.subscribe({
      next: () => this.passwordChangeForm.controls['confirmPassword'].updateValueAndValidity()
    })
}

  private matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }
  }

  cancel(){
    this.cancelResetEvent.emit(false);
  }
}
