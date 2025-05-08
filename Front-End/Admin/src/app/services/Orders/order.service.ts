import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { OrderDto, OrderDetails, OrderProduct, PaymentStatus, OrderStatus } from 'src/app/Models/Orders/OderDto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly API_URL = 'https://localhost:44341/api/Orders';

  constructor(private http: HttpClient) {}

  getAllOrders(): Observable<OrderDto[]> {
    return this.http.get<OrderDto[]>(`${this.API_URL}/OrdersCutomerName`);
  }

  getOrderDetails(id: string): Observable<OrderDetails> {
    return this.http.get<any>(`${this.API_URL}/Details/${id}`).pipe(
      map(response => this.transformOrderDetails(response))
    );
  }

  updateOrder(order: any): Observable<any> {
    debugger;
    return this.http.put(`${this.API_URL}`, order);
  }

  private transformOrderDetails(response: any): OrderDetails {
    return {
      ...response,
      orderData: response.orderData,
      customerName: `${response.customerFname || ''} ${response.customerMname || ''} ${response.customerLname || ''}`.trim(),
      orderProducts: response.orderProducts.map((product: any) => ({
        ...product,
        productCount: product.productCount || 1,
        quantity: product.productCount || 1
      }))
    };
  }

  getPaymentStatusString(status: number): string {
    return PaymentStatus[status] || 'Unknown';
  }

  getOrderStatusString(status: number): string {
    return OrderStatus[status] || 'Unknown';
  }
}