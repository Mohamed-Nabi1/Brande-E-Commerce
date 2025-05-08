import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CartService } from 'src/app/services/cart.service';
import { Router } from '@angular/router';
import { data } from 'jquery';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  cartItems: any[] = [];
  displayedColumns: string[] = [
    'image',
    'name',
    'price',
    'color',
    'size',
    'quantity',
    'actions',
  ];
  totalPriceBefore: any;
  totalPriceAfter: any;
  cart: any;
  checkOutBtn: any;
  constructor(private myService: CartService, public route: Router) {}

  ngOnInit() {
    this.myService.getCartProductsByCustomerId().subscribe(
      (response) => {
        this.cart = response;
        this.cartItems = response.products.map((item: any) => ({
          image: item.image || '',
          name: item.name,
          price: item.price,
          quantity: item.quantity,
          totalPriceBefor: item.price * item.quantity,
          totalPriceAfter: item.price * item.quantity * item.discount,
          quantityInStock: item.quantityInStock,
          color: item.color,
          size: item.size,
          productId: item.productId,
          cartId: response.cartId,
          discount: item.discount,
        }));

        this.calculateTotalPrice();
        if (this.cartItems.length) {
          this.checkOutBtn = this.cartItems.length;
        } else this.checkOutBtn = 0;
      },
      (error) => {
        if (error.status == 400) this.route.navigate(['/login']);
      }
    );
  }

  calculateTotalPrice() {
    if (this.cartItems.length != 0) {
      this.totalPriceBefore = this.cartItems.reduce(
        (total, item) => total + item.price * item.quantity, 0);
      this.cartItems.forEach((c) => {
        if (c.discount == 0) {
          this.totalPriceAfter = this.cartItems.reduce(
            (total, item) => total + item.price * item.quantity, 0);
        } else {
          this.totalPriceAfter = this.cartItems.reduce(
            (total, item) => total + (item.price * item.quantity * (1-item.discount)), 0);
          console.log(c.discount);
        }
      });
    } else {
      this.totalPriceBefore = 0;
      this.totalPriceAfter = 0;
    }
  }

  decreaseQuantity(item: any) {
    let product = {
      productId: item.productId,
      productCount: item.quantity,
      color: item.color,
      size: item.size,
    };
    console.log(product);
    this.myService.UpdateProductCountDecr(product).subscribe((data) => {
      console.log(data);
      console.log('data');
      this.ngOnInit();
    });
  }

  increaseQuantity(item: any) {
    let product = {
      productId: item.productId,
      productCount: item.quantity,
      color: item.color,
      size: item.size,
    };
    console.log(product);
    this.myService.UpdateProductCountPlus(product).subscribe((data) => {
      this.ngOnInit();
    });
  }

  deleteItem: any;
  deleteCartItem(item: any) {
    const cartId = 'c7d3e80a-7a4a-4c54-91a6-89c0df051c94';
    this.deleteItem = {
      cartId: item.cartId,
      productId: item.productId,
      color: item.color,
      size: item.size,
    };

    console.log(item.quantity);
    this.myService.deleteCartProduct(this.deleteItem).subscribe(
      () => {
        console.log('Item deleted');
        this.calculateTotalPrice(); // Recalculate the total price
        this.ngOnInit();
      },
      (error: any) => {
        console.error('Failed to delete item', error);
        this.calculateTotalPrice();
      }
    );
  }

  deleteCart() {
    console.log(this.cart.cartId);
    this.myService.deleteCart().subscribe(
      () => {
        console.log('Cart deleted');
        this.cartItems = [];
        this.ngOnInit();
      },
      (error: any) => {
        console.error('Failed to delete cart', error);
        this.calculateTotalPrice();
      }
    );
  }
}
