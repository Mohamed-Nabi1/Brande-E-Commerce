import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from 'src/app/services/Customer/customer.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  showPassword = false;

  message: any;
  displayAlert = 'd-none';
  FlagError = false;
  mess = false;
  constructor(
    protected _ser: CustomerService,
    private router: Router,
    private location: Location
  ) {}

  loginFrm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email,
      Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
    ]),
    password: new FormControl('', [Validators.required]),
  });

  keyPressMinLength(event: any, len: number) {
    let inp = event.target.value;
    inp.length < len ? true : event.preventDefault();
  }

  openDialog(mess: string, errFlag: boolean) {
    this.message = mess;
    this.FlagError = errFlag;
    this.displayAlert = 'd-block';
  }

  closePopup() {
    this.displayAlert = 'd-none';
  }
  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  login() {
    if (this.loginFrm.invalid) {
      // Display error messages
      Object.keys(this.loginFrm.controls).forEach((field) => {
        const control = this.loginFrm.get(field);
        if (control?.invalid) {
          control.markAsTouched({ onlySelf: true });
          console.log(control);
        }
      });
      console.log('I am Invalid');
    } else if (this.loginFrm.valid) {
      this._ser
        .login({
          email: this.loginFrm.value.email!,
          password: this.loginFrm.value.password!,
        })
        .subscribe({
          next: (response) => {
            // Store the token and expiration directly
            localStorage.setItem('token', response.token);
            localStorage.setItem('expiration', response.exp);

            // Set login state to true
            this._ser.isLoggedIn$.next(true);

            // Force a complete page reload to ensure all components are properly initialized
            window.location.href = window.location.origin + '/home';
          },
          error: () => {
            this.mess = true;
          },
        });
    }
  }
}
