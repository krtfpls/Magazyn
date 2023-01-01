import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.css']
})
export class NumberInputComponent implements ControlValueAccessor {

  @Input() label = '';
  @Input() type = 'number';
  @Input() min = '0';
  @Input() max = '9999';
  @Input() step = '1';
  
  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor = this;
  }
  
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  setDisabledState?(isDisabled: boolean): void {
  }
  
  get control(): FormControl {
    return this.ngControl.control as FormControl
  }
}