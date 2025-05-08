import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { CartService } from '../cart.service';

@Injectable({
  providedIn: 'root',
})
export class ProductDetailsService {
  constructor(
    private productData: HttpClient,
    private cartService: CartService
  ) { }

  baseUrl = 'https://localhost:44341/api/Products/';

  GetProductDetails(id: any) {
    return this.productData.get(this.baseUrl + 'ColorDistinct/' + id);
  }

  // FilterProductByColor(queryParam: any): Observable<any> {
  //   return this.productData.get(
  //     this.baseUrl + 'FillterByColor' + `?Id=${queryParam}`
  //   );
  // }

  AddtoCart(product: any) {
    return this.productData.post("https://localhost:44341/api/Cart/AddCart", product).pipe(
      tap(() => {
        // After adding a product to the cart, refresh the cart count
        this.cartService.getCartProductsByCustomerId().subscribe();
      })
    );
  }
}
