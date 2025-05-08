import { Component, OnInit } from '@angular/core';
import { OrderDetailsService } from '../order-details.service';
import { ActivatedRoute, Route } from '@angular/router';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  cartItems: any[] = [];

  displayedColumns: string[] = ['orderId', 'OrderDate' , 'PaymentStatue', 'TotalPrice'];
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
