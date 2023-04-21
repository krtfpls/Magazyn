import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private fb: FormBuilder,
          private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }
  
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }
  }

  register() {
    const value = this.registerForm.value;
    this.accountService.register(this.registerForm.value).subscribe({
      next:() => {
      this.router.navigateByUrl('/');
      this.toastr.success("Rejestracja przebiegła poprawnie. Sprawdź skrzynkę email i potwierdź rejestrację.")
    }, 
    error: error => {
      this.validationErrors = error
    }}
    )
  }

  cancel() {
    this.cancelRegister.emit(false);
  }


  private initializeForm(){
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      firstname: ['', [Validators.maxLength(100)]],
      lastname: ['', [Validators.maxLength(100)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(30)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });

    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })

  }
}
