import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  // BehaviorSubject to track cart items count
  private cartItemsCount = new BehaviorSubject<number>(0);
  public cartItemsCount$ = this.cartItemsCount.asObservable();

  constructor(private readonly myClient: HttpClient) {
    // Initialize cart count when service is created
    this.loadCartItemsCount();
  }
  private readonly API_URL = 'https://localhost:44341/api/';

  // Load cart items count from API
  private loadCartItemsCount() {
    this.getCartProductsByCustomerId().subscribe(
      (response) => {
        if (response && response.products) {
          this.cartItemsCount.next(response.products.length);
        } else {
          this.cartItemsCount.next(0);
        }
      },
      (error) => {
        console.error('Error loading cart count:', error);
        this.cartItemsCount.next(0);
      }
    );
  }

  // Get cart products with count update
  getCartProductsByCustomerId() {
    return this.myClient.get<any>(`${this.API_URL}cart/CartProducts`).pipe(
      tap((response) => {
        if (response && response.products) {
          this.cartItemsCount.next(response.products.length);
        }
      })
    );
  }

  deleteCartProduct(item: any) {
    const options = {
      body: item,
    };
    return this.myClient.delete(
      this.API_URL + 'Cart/DeletePrdouctFromCart',
      options
    ).pipe(
      tap(() => {
        // Update cart count after deleting an item
        this.loadCartItemsCount();
      })
    );
  }

  deleteCart() {
    return this.myClient.delete(`${this.API_URL}Cart`).pipe(
      tap(() => {
        // Reset cart count to zero after deleting the cart
        this.cartItemsCount.next(0);
      })
    );
  }

  updateCartProduct(productId: string, cartId: string, quantity: number) {
    const body = {
      ProductId: productId,
      CartId: cartId,
      Quantity: quantity,
    };
    return this.myClient.put<any>(`${this.API_URL}cart`, body).pipe(
      tap(() => {
        // Update cart count after updating a product
        this.loadCartItemsCount();
      })
    );
  }

  getProductDetails(productId: string) {
    return this.myClient.get<any>(
      `${this.API_URL}product/Details/${productId}`
    );
  }

  UpdateProductCountPlus(product: any) {
    return this.myClient.patch(`${this.API_URL}Cart/Increase`, product).pipe(
      tap(() => {
        // No need to update count as quantity change doesn't affect item count
      })
    );
  }

  UpdateProductCountDecr(product: any) {
    return this.myClient.patch(`${this.API_URL}Cart/Decrease`, product).pipe(
      tap(() => {
        // No need to update count as quantity change doesn't affect item count
      })
    );
  }

  // Method to manually update the cart count
  updateCartCount(count: number) {
    this.cartItemsCount.next(count);
  }
}
