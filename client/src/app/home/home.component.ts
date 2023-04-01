import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  resendMode = false;
  forgotMode = false;

  constructor() { }

  ngOnInit(): void {
    
  }

  forgotToggle(){
    this.forgotMode=true;
    this.resendMode = false;
    this.registerMode= false;
  }

  registerToggle() {
    this.registerMode= true;
    this.resendMode = false;
    this.forgotMode=false;
  }

  reSendEmailToggle(){
    this.resendMode= true;
    this.registerMode= false;   
    this.forgotMode=false;
  }

  cancelModes (event: boolean){
    this.registerMode= event;
    this.resendMode= event;
    this.forgotMode= event;
  }

}
