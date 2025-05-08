import { Component, OnInit } from '@angular/core';
import { OrderDetailsService } from '../order-details.service';
import { ActivatedRoute, Route } from '@angular/router';
import { OrderStatus, PaymentStatus } from '../order-dto.model'; // عدل المسار لو مختلف



  // باقي الكود...


@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
  
})
export class OrderDetailsComponent implements OnInit {
  OrderStatus = OrderStatus;
  PaymentStatus = PaymentStatus;
  cartItems: any[] = [];

  displayedColumns: string[] = ['id', 'arrivalDate' , 'orderStatus', 'products'];
  cart: any;

  constructor(private orderDetailsService: OrderDetailsService , private route: ActivatedRoute) {}

  ngOnInit() {
    this.orderDetailsService.getOrdersByCustomerId().subscribe(
      (response) => {
        // this.cartItems = response;
        // console.log(response);
        this.cartItems = Object.values(response);
        console.log(this.cartItems);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}