import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  constructor(private spinnerService: NgxSpinnerService) { }
  busy() {

    this.spinnerService.show(undefined, {
      type: 'square-jelly-box',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333',
    })
  }

  idle() {
    this.spinnerService.hide();
    
  }
}

