import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Email, passwordChangeModel, User, userProfile } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

baseUrl = environment.apiUrl+ 'account/';
private currentUserSource = new ReplaySubject<User | null>(1);
currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  verifyEmail(token: string, email: string) {
    return this.http.post(this.baseUrl + 'VerifyEmail', {token, email});
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'register', model)
    // .pipe(
    //   map(user => {
    //     if (user) {
    //      this.setCurrentUser(user);
    //     }
    //   })
    // )
  }

  changePassword(password: passwordChangeModel){
    return this.http.post<any>(this.baseUrl+'ChangePassword', password);
  }

  getUserProfile(){
    return this.http.get<userProfile>(this.baseUrl+'GetUserProfile');
  }

  resendEmail(email: Email){
    return this.http.post(this.baseUrl+'resendEmailConfirmationLink', email);
  }

  forgotPassword(email: Email){
    return this.http.post(this.baseUrl+'ForgotPassword', email);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
}
