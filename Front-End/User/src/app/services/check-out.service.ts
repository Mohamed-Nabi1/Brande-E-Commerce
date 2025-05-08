import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../Models/Order';

@Injectable({
  providedIn: 'root'
})
export class CheckOutService {
  private readonly BaseUrl = "https://localhost:44341/api/"
  constructor(private readonly myclient: HttpClient) {

  }

  GetCartProducts() {
    return this.myclient.get(`${this.BaseUrl}Cart/CartProducts`)
  }
  GetCustomer() {
    return this.myclient.get(`${this.BaseUrl}Customer/GetByOne`)
  }

  PlaceOrder(order: Order) {
    return this.myclient.post(`${this.BaseUrl}Orders`, order)
  }

  ClearCartProducts(customerId: any[]) {
    return this.myclient.delete(`${this.BaseUrl}Cart`)
  }
}
