import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

interface Customer {
  id: string;
  email: string;
  phoneNumber: string;
  firstName: string;
  midName: string;
  lastName: string;
  street: string;
  city: string;
  country: number;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private readonly API_URL = 'https://localhost:44341/api/Customer';

  constructor(private http: HttpClient) {}

  // Get all customers
  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.API_URL);
  }

  // Get single customer
  getCustomerById(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.API_URL}/GetByOne?id=${id}`);
  }
}