import { Component, OnInit } from '@angular/core';
import { CustomerService } from './services/Customer/customer.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(private authenticationService: CustomerService) {
    // localStorage.removeItem('token');
    // localStorage.removeItem('expiration');
  }

  ngOnInit(): void {
    // Check if token exists in localStorage and set login state
    const token = localStorage.getItem('token');
    const expiration = localStorage.getItem('expiration');

    if (token && expiration) {
      console.log('Token found in localStorage, setting login state to true');

      // Check if token is expired
      if (new Date(expiration) > new Date()) {
        // Token is valid
        this.authenticationService.isLoggedIn$.next(true);
      } else {
        // Token is expired, clear it
        console.log('Token expired, clearing it');
        localStorage.removeItem('token');
        localStorage.removeItem('expiration');
        this.authenticationService.isLoggedIn$.next(false);
      }
    } else {
      console.log('No token found in localStorage, setting login state to false');
      this.authenticationService.isLoggedIn$.next(false);
    }
  }
  title = 'Front_ITI_GP';
}
