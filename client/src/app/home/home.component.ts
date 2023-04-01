import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  resendMode = false;

  constructor() { }

  ngOnInit(): void {
    
  }

  registerToggle() {
    this.registerMode= true;
    this.resendMode = false;
  }

  reSendEmailToggle(){
    this.resendMode= true;
    this.registerMode= false;
  }

  cancelModes (event: boolean){
    this.registerMode= event;
    this.resendMode= event;
  }

}
