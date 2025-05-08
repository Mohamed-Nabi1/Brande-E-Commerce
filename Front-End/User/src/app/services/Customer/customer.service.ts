import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginDto } from 'src/app/Models/LoginDto';
import { TokenDto } from 'src/app/Models/TokenDto';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  public isLoggedIn$ = new BehaviorSubject<boolean>(false);
  // ==============================================================
  constructor(private http: HttpClient, private router: Router) {}
  private readonly API_URL = 'https://localhost:44341/api/';

  GetCustomer() {
    return this.http.get(`${this.API_URL}Customer/GetByOne`);
  }
  AddCustomer(data: any) {
    return this.http.post(this.API_URL + 'Security/Register', data);
  }
  LoginCustomer(data: any) {
    return this.http.post(this.API_URL + 'Security/Login', data);
  }
  ResetPassword(data: any) {
    return this.http.patch(this.API_URL + 'Customer/RestPassword', data);
  }
  // ======================================================================
  public login(credentials: LoginDto): Observable<TokenDto> {
    return this.http
      .post<TokenDto>(this.API_URL + 'Security/Login', credentials)
      .pipe(
        tap((tokenDto) => {
          // Store token in localStorage
          localStorage.setItem('token', tokenDto.token);
          localStorage.setItem('expiration', tokenDto.exp);

          // Update login state
          this.isLoggedIn$.next(true);

          console.log('Login successful, token stored and login state updated');
        })
      );
  }
  public getTestData(): Observable<string[]> {
    return this.http.get<string[]>(this.API_URL + 'Test/test');
  }

  UpdateCustomerByID(customerUpdate: any) {
    console.log(customerUpdate);
    return this.http.patch(this.API_URL + `Customer`, customerUpdate);
  }

  public logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('expiration');
    this.isLoggedIn$.next(false);

    // Force a complete page reload to ensure all components are properly reset
    window.location.href = window.location.origin + '/home';
  }
}
