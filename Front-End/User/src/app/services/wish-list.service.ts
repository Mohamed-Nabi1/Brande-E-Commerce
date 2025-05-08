import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class WishListService {
  // BehaviorSubject to track wishlist items count
  private wishlistItemsCount = new BehaviorSubject<number>(0);
  public wishlistItemsCount$ = this.wishlistItemsCount.asObservable();

  // BehaviorSubject to track wishlist product IDs
  private wishlistProductIds = new BehaviorSubject<string[]>([]);
  public wishlistProductIds$ = this.wishlistProductIds.asObservable();

  constructor(private readonly myClient: HttpClient) {
    // Initialize wishlist when service is created
    this.loadWishlist();
  }
  private readonly API_URL = 'https://localhost:44341/api/';

  // Load wishlist from API
  private loadWishlist() {
    this.getWishListByCustomerId().subscribe(
      (response) => {
        if (response && response.products) {
          this.wishlistItemsCount.next(response.products.length);

          // Extract product IDs and store them
          const productIds = response.products.map((product: any) => product.productId);
          this.wishlistProductIds.next(productIds);
        } else {
          this.wishlistItemsCount.next(0);
          this.wishlistProductIds.next([]);
        }
      },
      (error) => {
        console.error('Error loading wishlist:', error);
        this.wishlistItemsCount.next(0);
        this.wishlistProductIds.next([]);
      }
    );
  }

  getWishListByCustomerId() {
    return this.myClient.get<any>(`${this.API_URL}WishLists`).pipe(
      tap((response) => {
        if (response && response.products) {
          this.wishlistItemsCount.next(response.products.length);

          // Extract product IDs and store them
          const productIds = response.products.map((product: any) => product.productId);
          this.wishlistProductIds.next(productIds);
        }
      })
    );
  }

  addProductInWishlist(data: any) {
    return this.myClient.patch(`${this.API_URL}WishLists/add`, data).pipe(
      tap(() => {
        // Update wishlist after adding an item
        this.loadWishlist();
      })
    );
  }

  deleteProductInWishlist(item: any) {
    const options = {
      productId: item,
    };
    return this.myClient.patch(`${this.API_URL}WishLists/delete`, options).pipe(
      tap(() => {
        // Update wishlist after deleting an item
        this.loadWishlist();
      })
    );
  }

  // Check if a product is in the wishlist
  isProductInWishlist(productId: string): boolean {
    const currentWishlistIds = this.wishlistProductIds.getValue();
    return currentWishlistIds.includes(productId);
  }

  // Toggle product in wishlist (add if not present, remove if present)
  toggleProductInWishlist(productId: string): Observable<any> {
    if (this.isProductInWishlist(productId)) {
      return this.deleteProductInWishlist(productId);
    } else {
      return this.addProductInWishlist({ productId });
    }
  }

  // Method to manually update the wishlist count
  updateWishlistCount(count: number) {
    this.wishlistItemsCount.next(count);
  }
}
