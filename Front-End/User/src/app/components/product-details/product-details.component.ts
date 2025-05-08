import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductDetailsService } from 'src/app/services/Product Details/product-details.service';
import { Location } from '@angular/common';
import { WishListService } from 'src/app/services/wish-list.service';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit {
  constructor(
    private productService: ProductDetailsService,
    private urlData: ActivatedRoute,
    private location: Location,
    private route: Router,
    private wishListService: WishListService,
    private renderer: Renderer2,
    private el: ElementRef
  ) {}

  productId = this.urlData.snapshot.params['id'];

  product: any;
  ColorSizes: any;
  maxQuantity: any;
  Quantity: number = 1;
  minQuantity: number = 1;
  colorValue: any;
  ImageCount: any;
  addCart: any;
  size: any;

  selectedColor: string = '';
  selectedSize: any;
  validationMessage: string = '';
  isAddedToWishlist = false;
  private wishlistSubscription: any;

  ngOnInit(): void {
    this.productService.GetProductDetails(this.productId).subscribe(
      (data) => {
        this.product = data;
        this.ColorSizes = this.product.productInfo[0];
        this.colorValue = null; // تعطيل اختيار اللون الافتراضي
        this.maxQuantity = this.product.productInfo[0].sizeQuantities[0].quantity;
        this.ImageCount = this.product.productImages.Count;
      },
      (error) => {
        if (error.status == 401) this.route.navigate(['/']);
      }
    );
  
    this.wishlistSubscription = this.wishListService.wishlistProductIds$.subscribe(
      (productIds) => {
        this.isAddedToWishlist = productIds.includes(this.productId);
        console.log('Product in wishlist:', this.isAddedToWishlist);
      }
    );
  }
  
  Sizes(value: any) {
    this.Quantity = 1;
    this.colorValue = value;
    for (let i = 0; i < this.product.productInfo.length; i++) {
      if (this.product.productInfo[i].color == value) {
        this.ColorSizes = this.product.productInfo[i];
      }
    }
  }

  CheckMax() {
    if (this.Quantity < this.maxQuantity) this.Quantity++;
  }

  CheckMin() {
    if (this.Quantity > this.minQuantity) this.Quantity--;
  }

  QuantityCheck(e: any, size: any) {
    this.Quantity = 1;
    this.size = size;
    for (let i = 0; i < this.product.productInfo.length; i++) {
      if (this.product.productInfo[i].color == this.colorValue) {
        for (let j = 0; j < this.product.productInfo[i].sizeQuantities.length; j++) {
          if (this.product.productInfo[i].sizeQuantities[j].size == size) {
            this.maxQuantity = this.product.productInfo[i].sizeQuantities[j].quantity;
          }
        }
      }
    }
  }

  validateInputs(): boolean {
    // تحقق من أن اللون والمقاس تم اختيارهما
    if (!this.colorValue || !this.size) {
      this.showValidationMessage('Please select both color and size');
      return false;
    }
  
    // تحقق من الكمية
    if (this.Quantity > this.maxQuantity) {
      this.showValidationMessage('Selected quantity is not available');
      return false;
    }
  
    return true;  // إذا كانت جميع المدخلات صحيحة
  }
  
  

  showValidationMessage(message: string) {
    this.validationMessage = message;

    const modalElement = this.el.nativeElement.querySelector('#ValidationModal');
    const modal = new bootstrap.Modal(modalElement);
    this.renderer.addClass(modalElement, 'show');
    this.renderer.setStyle(modalElement, 'display', 'block');
    modal.show();

    setTimeout(() => {
      this.renderer.removeClass(modalElement, 'show');
      this.renderer.setStyle(modalElement, 'display', 'none');
      modal.hide();
    }, 2000);
  }

  AddToCart() {
    // التحقق إذا كان اللون والمقاس تم اختيارهما
    if (!this.validateInputs()) return;
  
    // إذا كانت المدخلات صحيحة، نكمل عملية إضافة المنتج إلى السلة
    this.addCart = {
      productId: this.product.id,
      productCount: this.Quantity,
      color: this.colorValue,
      size: this.size,
    };
  
    this.productService.AddtoCart(this.addCart).subscribe(
      (response) => {
        // إظهار المودال عند إضافة المنتج إلى السلة
        const modalElement = this.el.nativeElement.querySelector('#AddedToCartModal');
        const modal = new bootstrap.Modal(modalElement);
        this.renderer.addClass(modalElement, 'show');
        this.renderer.setStyle(modalElement, 'display', 'block');
        modal.show();
  
        setTimeout(() => {
          this.renderer.removeClass(modalElement, 'show');
          this.renderer.setStyle(modalElement, 'display', 'none');
          modal.hide();
        }, 1000);
      },
      (error) => {
        if (error.status === 401) {
          this.route.navigate(['/login']);
        }
      }
    );
  
    console.log(this.addCart);
  }
  

  selectColor(color: string) {
    this.selectedColor = color;
  }

  selectSize(size: string) {
    if (this.selectedSize === size) {
      this.selectedSize = null;
    } else {
      this.selectedSize = size;
    }
  }

  GoBack() {
    this.route.navigate(['/products']);
  }

  addToWishlist() {
    this.wishListService.toggleProductInWishlist(this.productId).subscribe(
      () => {
        console.log('Wishlist toggled successfully');
      },
      (error) => {
        console.error('Error toggling wishlist:', error);
        if (error.status === 401) {
          this.route.navigate(['/login']);
        }
      }
    );
  }

  ngOnDestroy() {
    if (this.wishlistSubscription) {
      this.wishlistSubscription.unsubscribe();
    }
  }
}
