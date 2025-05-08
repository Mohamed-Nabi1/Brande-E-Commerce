import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  constructor(private http: HttpClient) { }

  getOrdersByCustomerId() {
    return this.http.get('https://localhost:44341/api/Orders/ByCustomer');
  }
  updateOrder(order: any) {
    return this.http.put('https://localhost:44341/api/Orders', order);
  } cancelOrder(order: any) {
    return this.http.put('https://localhost:44341/api/Orders', name);
  }



}
