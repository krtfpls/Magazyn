import { Component, OnInit } from '@angular/core';
import { userProfile } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  user: userProfile | undefined;
  passwordMode = false;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.getUserProfile();
  }


changePasswordMode(){
  this.passwordMode = !this.passwordMode;
}
  private getUserProfile(){
    this.accountService.getUserProfile().subscribe({
      next: (result) => {
        this.user = result;
      }})
  }
}
