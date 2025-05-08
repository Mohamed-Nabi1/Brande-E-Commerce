import { Component, OnInit, OnDestroy, ViewChild, inject, HostListener, ElementRef, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from 'src/app/services/Customer/customer.service';
import { CartService } from 'src/app/services/cart.service';
import { WishListService } from 'src/app/services/wish-list.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  constructor(
    private customerService: CustomerService,
    private cartService: CartService,
    private wishListService: WishListService,
    private router: Router,
    private renderer: Renderer2,
    private el: ElementRef
  ) {}

  public isLoggedIn: boolean = false;
  public customer: any = null;
  public isLoadingCustomer: boolean = false;
  public searchQuery: string = '';
  public isScrolled: boolean = false;
  public cartItemsCount: number = 0;
  public wishlistItemsCount: number = 0;
  public isMobileNavVisible: boolean = false;
  private loginSubscription: any;
  private cartSubscription: any;
  private wishlistSubscription: any;
  private tokenCheckInterval: any;

  @ViewChild('selectList') selectList: any;

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const scrollPosition = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;

    if (scrollPosition > 50) {
      this.isScrolled = true;
      const navbar = this.el.nativeElement.querySelector('.navbar');
      if (navbar) {
        this.renderer.addClass(navbar, 'scrolled');
      }
    } else {
      this.isScrolled = false;
      const navbar = this.el.nativeElement.querySelector('.navbar');
      if (navbar) {
        this.renderer.removeClass(navbar, 'scrolled');
      }
    }
  }

  ngOnInit(): void {
    // Set loading state
    this.isLoadingCustomer = true;

    // Subscribe to cart items count
    this.cartSubscription = this.cartService.cartItemsCount$.subscribe(
      (count) => {
        this.cartItemsCount = count;
      }
    );

    // Subscribe to wishlist items count
    this.wishlistSubscription = this.wishListService.wishlistItemsCount$.subscribe(
      (count) => {
        this.wishlistItemsCount = count;
      }
    );

    // Check if token exists in localStorage
    const token = localStorage.getItem('token');
    if (token) {
      console.log('Token found in localStorage');
      this.isLoggedIn = true;
      this.customerService.isLoggedIn$.next(true);

      // Immediately fetch customer data
      this.fetchCustomerData();
    } else {
      console.log('No token found in localStorage');
      this.isLoggedIn = false;
      this.customerService.isLoggedIn$.next(false);
      this.isLoadingCustomer = false;
    }

    // Subscribe to login status changes
    this.loginSubscription = this.customerService.isLoggedIn$.subscribe(
      (logIn) => {
        console.log('Login state changed:', logIn);
        this.isLoggedIn = logIn;

        if (logIn) {
          // Fetch customer data when login state changes to true
          this.fetchCustomerData();
        } else {
          // Clear customer data when logged out
          this.customer = null;
          this.isLoadingCustomer = false;
        }
      },
      (error) => {
        console.error('Error checking login status:', error);
        this.isLoadingCustomer = false;
      }
    );

    // Set up interval to check token expiration
    this.tokenCheckInterval = setInterval(() => {
      const currentToken = localStorage.getItem('token');
      const expiration = localStorage.getItem('expiration');

      if (currentToken && expiration) {
        // Check if token is expired
        if (new Date(expiration) < new Date()) {
          console.log('Token expired, logging out');
          localStorage.removeItem('token');
          localStorage.removeItem('expiration');
          this.isLoggedIn = false;
          this.customerService.isLoggedIn$.next(false);
        }
      } else if (this.isLoggedIn) {
        // Token is missing but we're logged in
        console.log('Token missing but logged in state is true, fixing...');
        this.isLoggedIn = false;
        this.customerService.isLoggedIn$.next(false);
      }
    }, 5000); // Check every 5 seconds
  }

  /**
   * Fetch customer data from the API
   */
  fetchCustomerData() {
    console.log('Fetching customer data...');
    this.isLoadingCustomer = true;

    this.customerService.GetCustomer().subscribe({
      next: (data) => {
        console.log('Customer data received:', data);
        this.customer = data;
        this.isLoadingCustomer = false;
      },
      error: (error) => {
        console.error('Error fetching customer data:', error);
        this.isLoadingCustomer = false;

        // If we get a 401 (unauthorized), the token might be invalid or expired
        if (error.status === 401) {
          // Clear token and update login state
          localStorage.removeItem('token');
          localStorage.removeItem('expiration');
          this.isLoggedIn = false;
          this.customerService.isLoggedIn$.next(false);
        }
      }
    });
  }

  /**
   * Handle user logout
   */
  logOut() {
    this.customerService.logout();
    this.isLoggedIn = false;
    this.customer = null;
  }

  /**
   * Search for products
   */
  search() {
    if (this.searchQuery.trim()) {
      console.log('Searching for:', this.searchQuery);
      // Navigate to the products page with the search query as a parameter
      this.router.navigate(['/products'], { queryParams: { search: this.searchQuery.trim() } });
      // Clear the search query after navigation
      this.searchQuery = '';
    }
  }

  /**
   * Navigate to category page
   */
  navigateToCategory(categoryId: string) {
    console.log('Navigating to category:', categoryId);

    // Log the category name for debugging
    if (categoryId === 'edc6b9e0-9252-4e9d-b4d3-9203b6de2583') {
      console.log('Category: Men');
    } else if (categoryId === 'a6c4de53-33c5-48e1-9f21-5649726d2a3d') {
      console.log('Category: Women');
    } else if (categoryId === '52d40b0a-7039-4bc6-899d-0c36adff6b8f') {
      console.log('Category: Kids');
    }

    // Navigate to the category page
    console.log(`Navigating to: /category/${categoryId}`);
    this.router.navigate(['/category', categoryId]);
  }

  /**
   * Navigate to all products page
   */
  navigateToAllProducts() {
    this.router.navigate(['/products']);
  }

  /**
   * Legacy method for dropdown navigation
   */
  Go(e: any) {
    if (e.target.value == 1) {
      this.router.navigate(["category/edc6b9e0-9252-4e9d-b4d3-9203b6de2583"]); // Men
      if (this.selectList && this.selectList.nativeElement) {
        this.selectList.nativeElement.selectedIndex = 0;
      }
    }
    else if (e.target.value == 2) {
      this.router.navigate(['category/a6c4de53-33c5-48e1-9f21-5649726d2a3d']); // Women
      if (this.selectList && this.selectList.nativeElement) {
        this.selectList.nativeElement.selectedIndex = 0;
      }
    }
    else if (e.target.value == 3) {
      this.router.navigate(["category/52d40b0a-7039-4bc6-899d-0c36adff6b8f"]); // Kids
      if (this.selectList && this.selectList.nativeElement) {
        this.selectList.nativeElement.selectedIndex = 0;
      }
    }
  }

  /**
   * Toggle mobile navigation visibility
   */
  toggleMobileNav() {
    this.isMobileNavVisible = !this.isMobileNavVisible;
    console.log('Mobile nav visibility:', this.isMobileNavVisible);
  }

  /**
   * Clean up resources when component is destroyed
   */
  ngOnDestroy(): void {
    // Unsubscribe from login subscription
    if (this.loginSubscription) {
      this.loginSubscription.unsubscribe();
    }

    // Unsubscribe from cart subscription
    if (this.cartSubscription) {
      this.cartSubscription.unsubscribe();
    }

    // Unsubscribe from wishlist subscription
    if (this.wishlistSubscription) {
      this.wishlistSubscription.unsubscribe();
    }

    // Clear the token check interval
    if (this.tokenCheckInterval) {
      clearInterval(this.tokenCheckInterval);
    }
  }
}
