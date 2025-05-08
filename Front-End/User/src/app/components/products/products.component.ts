import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { isEmptyObject } from 'jquery';
import { Filter } from 'src/app/Models/Products/Filter';
import { ProductDetails } from 'src/app/Models/Products/Product';
import { ProductsService } from 'src/app/services/Product/products.service';
import { CategoryService } from 'src/app/services/category.service';
import { WishListService } from 'src/app/services/wish-list.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  constructor(
    private http: HttpClient,
    public myService: ProductsService,
    public route: ActivatedRoute,
    public categoryService: CategoryService,
    private router: Router,
    private wishListService: WishListService
  ) {}

  // parentCategories: any;
  // subcategories: any;
  // productsFromCategory: any;
  // ///;
  // ID: any;
  // products: any[] = [];
  // page: number = 1;
  // totalRecords: number = 0;
  // categoryId: any;
  // filterCategory: any;
  // totalCount = 0;
  // showButton: boolean = false;
  // isLoading = false;
  // isLoadingg = false;
  // items: any;
  // itemsURL: [] = [];

  // counterPerPage = 10;
  CategoryId: string = '';
  subCategories: any;
  productsofSub: any;
  arrrry: any[] = [];
  subCategoryId: any;
  Color: any;
  Size: any;
  originSubCateories: any;
  originproducts: any[] = [];
  Images: any;
  isAllProducts: boolean = false;
  categoryName: string = '';
  categoryType: string = '';
  breadcrumbPath: string = 'Home / Products';
  private routeSubscription: any;
  private queryParamSubscription: any;
  private wishlistSubscription: any;
  searchQuery: string = '';
  isSearching: boolean = false;
  wishlistProductIds: string[] = []; // Store wishlist product IDs

  // Pagination properties
  currentPage: number = 1;
  itemsPerPage: number = 9; // Number of products per page
  totalPages: number = 1;
  allProducts: any[] = []; // Store all products before pagination

  filter: Filter = {
    subCategories: [],
    color: [],
    size: [],
  };

  // View mode property (grid or list)
  viewMode: string = 'grid';

  async ngOnInit() {
    // Check if there's a saved view mode preference
    const savedViewMode = localStorage.getItem('productViewMode');
    if (savedViewMode) {
      this.viewMode = savedViewMode;
    }

    // Subscribe to wishlist product IDs
    this.wishlistSubscription = this.wishListService.wishlistProductIds$.subscribe(
      (productIds) => {
        this.wishlistProductIds = productIds;
        console.log('Wishlist product IDs updated:', this.wishlistProductIds);
      }
    );

    // Subscribe to query parameter changes to handle search
    this.queryParamSubscription = this.route.queryParamMap.subscribe(queryParams => {
      const searchParam = queryParams.get('search');
      if (searchParam) {
        this.searchQuery = searchParam;
        this.isSearching = true;
        console.log('Search query detected:', this.searchQuery);

        // When searching, we want to show all products and then filter them
        this.isAllProducts = true;
        this.categoryName = `Search Results for "${this.searchQuery}"`;
        this.breadcrumbPath = `Home / Search Results`;

        // Load all products and then filter them by search query
        this.getAllProductsAndSearch();
      } else {
        // Reset search state if no search parameter
        this.searchQuery = '';
        this.isSearching = false;
      }
    });

    // Subscribe to route parameter changes and route data
    this.routeSubscription = this.route.paramMap.subscribe(params => {
      // If we're searching, don't process route parameters
      if (this.isSearching) {
        return;
      }

      // Clear previous data
      this.productsofSub = [];
      this.categoryName = '';
      this.categoryType = '';
      this.originproducts = [];
      this.originSubCateories = [];
      this.subCategories = [];

      // Check if we're on a route with data (products/men, products/women, products/kids)
      const routeData = this.route.snapshot.data;

      if (routeData && routeData['categoryId']) {
        // We're on one of the new routes (products/men, products/women, products/kids)
        this.CategoryId = routeData['categoryId'];
        this.categoryName = routeData['categoryName'] || '';
        console.log(`Route changed to ${this.categoryName} products, CategoryId:`, this.CategoryId);

        // Set isAllProducts flag to false since we're on a category page
        this.isAllProducts = false;
        console.log('Is All Products page:', this.isAllProducts);

        // Load products for this category
        this.loadCategoryProducts(this.categoryName);

        // Set breadcrumb path
        this.breadcrumbPath = `Home / Products / ${this.categoryName}`;

      } else {
        // Get the category ID from the route parameters
        this.CategoryId = params.get('id') || '';
        console.log('Route changed, new CategoryId:', this.CategoryId);

        // Check if we're on the "All Products" route
        this.isAllProducts = !this.CategoryId;
        console.log('Is All Products page:', this.isAllProducts, 'CategoryId:', this.CategoryId);

        if (this.isAllProducts) {
          console.log('Loading All Products page');
          // Get all products
          this.getAllProducts();

          // Set breadcrumb path for All Products
          this.breadcrumbPath = 'Home / Products';

        } else {
          // Check if this is a parent category (Men, Women, Kids)
          if (this.CategoryId === 'edc6b9e0-9252-4e9d-b4d3-9203b6de2583') { // Men
            console.log('Loading Men products');
            // Set isAllProducts flag to false since we're on a category page
            this.isAllProducts = false;
            this.loadCategoryProducts('Men');

            // Set breadcrumb path
            this.breadcrumbPath = 'Home / Products / Men';

          } else if (this.CategoryId === 'a6c4de53-33c5-48e1-9f21-5649726d2a3d') { // Women
            console.log('Loading Women products');
            // Set isAllProducts flag to false since we're on a category page
            this.isAllProducts = false;
            this.loadCategoryProducts('Women');

            // Set breadcrumb path
            this.breadcrumbPath = 'Home / Products / Women';

          } else if (this.CategoryId === '52d40b0a-7039-4bc6-899d-0c36adff6b8f') { // Kids
            console.log('Loading Kids products');
            // Set isAllProducts flag to false since we're on a category page
            this.isAllProducts = false;
            this.loadCategoryProducts('Kids');

            // Set breadcrumb path
            this.breadcrumbPath = 'Home / Products / Kids';

          } else {
            // Try other methods for subcategories or unknown categories
            // Set isAllProducts flag to false since we're on a category page
            this.isAllProducts = false;
            this.getProductsByCategory();
          }
        }
      }
    });
  }

  loadCategoryProducts(categoryName: string) {
    // Set the category name immediately for better UX
    this.categoryName = categoryName;

    // Set breadcrumb path
    this.breadcrumbPath = `Home / Products / ${categoryName}`;

    // Reset pagination
    this.currentPage = 1;
    this.allProducts = [];

    // Set isAllProducts flag to false since we're on a category page
    this.isAllProducts = false;
    console.log('Is All Products page:', this.isAllProducts);

    console.log(`Loading ${categoryName} products with ID: ${this.CategoryId}`);

    // DIRECT APPROACH: Use the specific endpoint for getting products by parent category ID
    this.myService.getProductsByParentCategoryId(this.CategoryId).subscribe({
      next: (products: any) => {
        console.log(`${categoryName} products response:`, products);

        if (products && Array.isArray(products) && products.length > 0) {
          console.log(`SUCCESS: Got ${products.length} ${categoryName} products`);

          // Store all products for pagination
          this.allProducts = products;

          // Store the original products so we can restore them when clearing filters
          this.originalAllProducts = [...products];

          // Apply pagination
          this.applyPagination();

          // Get the current sort option from localStorage or dropdown
          const savedSortOption = localStorage.getItem('productSortOption');
          if (savedSortOption) {
            // Apply the saved sort option
            this.sortProducts(savedSortOption);

            // Update the dropdown to reflect the current sort option
            const sortSelect = document.querySelector('.sort-options select') as HTMLSelectElement | null;
            if (sortSelect) {
              sortSelect.value = savedSortOption;
            }
          } else {
            // No saved sort option, check the dropdown
            const sortSelect = document.querySelector('.sort-options select') as HTMLSelectElement | null;
            if (sortSelect && sortSelect.value !== 'featured') {
              // Apply sorting based on the selected option
              this.sortProducts(sortSelect.value);
            }
          }

          // Also get subcategories for filtering
          this.getSubcategoriesForCategory();
        } else {
          console.log(`No ${categoryName} products found, trying alternative approach`);
          this.getProductsAlternative();
        }
      },
      error: (err) => {
        console.log(`Error getting ${categoryName} products:`, err);
        this.getProductsAlternative();
      }
    });
  }

  getProductsAlternative() {
    console.log('Trying alternative approach to get products');

    // Reset pagination
    this.currentPage = 1;
    this.allProducts = [];

    // Set isAllProducts flag to false since we're on a category page
    this.isAllProducts = false;
    console.log('Is All Products page:', this.isAllProducts);

    // Get subcategories first
    this.myService.getSubcategories(this.CategoryId).subscribe({
      next: (subcategories: any) => {
        console.log('Subcategories response:', subcategories);

        if (subcategories && Array.isArray(subcategories) && subcategories.length > 0) {
          this.originSubCateories = subcategories;
          this.subCategories = this.originSubCateories;

          // Extract and combine products from all subcategories
          let combinedProducts: any[] = [];

          subcategories.forEach((subcat: any) => {
            if (subcat.products && Array.isArray(subcat.products) && subcat.products.length > 0) {
              combinedProducts = combinedProducts.concat(subcat.products);
            }
          });

          if (combinedProducts.length > 0) {
            console.log(`SUCCESS: Got ${combinedProducts.length} products from subcategories`);

            // Store all products for pagination
            this.allProducts = combinedProducts;

            // Store the original products so we can restore them when clearing filters
            this.originalAllProducts = [...combinedProducts];

            // Apply pagination
            this.applyPagination();

            this.originproducts = subcategories
              .map((p: { products: any }) => p.products)
              .filter((p: any) => !isEmptyObject(p));
          } else {
            console.log('No products found in subcategories');
            this.productsofSub = [];
            this.allProducts = [];
            this.originalAllProducts = [];
          }
        } else {
          console.log('No subcategories found');
          this.productsofSub = [];
          this.allProducts = [];
          this.originalAllProducts = [];
        }
      },
      error: (err) => {
        console.log('Error getting subcategories:', err);
        this.productsofSub = [];
        this.allProducts = [];
        this.originalAllProducts = [];
      }
    });
  }

  getSubcategoriesForCategory() {
    // Get subcategories for filtering
    this.myService.getSubcategories(this.CategoryId).subscribe({
      next: (subcategories: any) => {
        console.log('Subcategories for filtering:', subcategories);
        this.originSubCateories = subcategories;
        this.subCategories = this.originSubCateories;

        // Extract products from subcategories for filtering
        if (subcategories && Array.isArray(subcategories) && subcategories.length > 0) {
          this.originproducts = subcategories
            .map((p: { products: any }) => p.products)
            .filter((p: any) => !isEmptyObject(p));
        }
      },
      error: (err) => {
        console.log('Error fetching subcategories for filtering:', err);
      }
    });
  }



  ngOnDestroy() {
    // Unsubscribe to prevent memory leaks
    if (this.routeSubscription) {
      this.routeSubscription.unsubscribe();
    }
    if (this.queryParamSubscription) {
      this.queryParamSubscription.unsubscribe();
    }
    if (this.wishlistSubscription) {
      this.wishlistSubscription.unsubscribe();
    }
  }

  // Method to load all products and filter them by search query
  getAllProductsAndSearch() {
    console.log('Loading all products for search:', this.searchQuery);

    // Reset pagination
    this.currentPage = 1;
    this.allProducts = [];
    this.originalAllProducts = [];

    // Get all parent categories first
    this.categoryService.GetParentCategories().subscribe({
      next: (parentCategories: any) => {
        console.log('Parent categories loaded for search:', parentCategories.length);

        // For each parent category, get its products
        let combinedProducts: any[] = [];
        let processedCategories = 0;

        // Also get all subcategories for filtering
        this.loadAllSubcategories();

        parentCategories.forEach((category: any) => {
          console.log('Loading products for parent category:', category.name, category.id);

          this.myService.getProductsByParentCategoryId(category.id).subscribe({
            next: (products: any) => {
              if (products && Array.isArray(products)) {
                console.log(`Loaded ${products.length} products for category ${category.name}`);

                // Add category information to each product
                const productsWithCategory = products.map((product: any) => {
                  // If the product doesn't have a categories array, create one
                  if (!product.categories || !Array.isArray(product.categories)) {
                    product.categories = [];
                  }

                  // Add this category to the product's categories if it's not already there
                  const hasCategory = product.categories.some((cat: any) => cat.id === category.id);
                  if (!hasCategory) {
                    product.categories.push({
                      id: category.id,
                      name: category.name
                    });
                  }

                  return product;
                });

                combinedProducts = combinedProducts.concat(productsWithCategory);
              }
              processedCategories++;

              // When all categories are processed, filter products by search query
              if (processedCategories === parentCategories.length) {
                console.log(`All products loaded for search: ${combinedProducts.length} products`);

                // Filter products by search query
                const filteredProducts = this.filterProductsBySearchQuery(combinedProducts, this.searchQuery);
                console.log(`Filtered products by search query: ${filteredProducts.length} products`);

                // Store all filtered products for pagination
                this.allProducts = filteredProducts;

                // Store the original filtered products
                this.originalAllProducts = [...filteredProducts];

                // Apply pagination to show the first page
                this.applyPagination();

                console.log('Total filtered products:', filteredProducts.length);
                console.log('Products on first page:', this.productsofSub.length);
                console.log('Total pages:', this.totalPages);
              }
            },
            error: (err) => {
              console.log('Error fetching products for category:', category.name, err);
              processedCategories++;

              // Even if there's an error, check if all categories have been processed
              if (processedCategories === parentCategories.length) {
                console.log(`Finished loading with some errors. Total products: ${combinedProducts.length}`);

                // Filter products by search query
                const filteredProducts = this.filterProductsBySearchQuery(combinedProducts, this.searchQuery);
                console.log(`Filtered products by search query: ${filteredProducts.length} products`);

                // Store all filtered products for pagination
                this.allProducts = filteredProducts;

                // Store the original filtered products
                this.originalAllProducts = [...filteredProducts];

                // Apply pagination to show the first page
                this.applyPagination();
              }
            }
          });
        });
      },
      error: (err) => {
        console.log('Error fetching parent categories for search:', err);
        this.productsofSub = [];
        this.allProducts = [];
        this.originalAllProducts = [];
      }
    });
  }

  // Helper method to filter products by search query
  filterProductsBySearchQuery(products: any[], query: string): any[] {
    if (!query || !products || !Array.isArray(products)) {
      return products;
    }

    // Convert query to lowercase for case-insensitive comparison
    const lowerCaseQuery = query.toLowerCase();

    return products.filter(product => {
      // Check if product name contains the search query
      if (product.name && product.name.toLowerCase().includes(lowerCaseQuery)) {
        return true;
      }

      // Check if product description contains the search query
      if (product.description && product.description.toLowerCase().includes(lowerCaseQuery)) {
        return true;
      }

      // Check if any category name contains the search query
      if (product.categories && Array.isArray(product.categories)) {
        for (const category of product.categories) {
          if (category.name && category.name.toLowerCase().includes(lowerCaseQuery)) {
            return true;
          }
        }
      }

      // No match found
      return false;
    });
  }

  // Store the original products for the All Products page
  originalAllProducts: any[] = [];

  getAllProducts() {
    console.log('Loading all products for All Products page');

    // Set isAllProducts flag to true since we're on the All Products page
    this.isAllProducts = true;
    console.log('Is All Products page:', this.isAllProducts);

    // Reset pagination
    this.currentPage = 1;
    this.allProducts = [];
    this.originalAllProducts = [];

    // Set breadcrumb path for All Products
    this.breadcrumbPath = 'Home / Products';

    // Get all parent categories first
    this.categoryService.GetParentCategories().subscribe({
      next: (parentCategories: any) => {
        console.log('Parent categories loaded:', parentCategories.length);

        // For each parent category, get its products
        let combinedProducts: any[] = [];
        let processedCategories = 0;

        // Also get all subcategories for filtering
        this.loadAllSubcategories();

        parentCategories.forEach((category: any) => {
          console.log('Loading products for parent category:', category.name, category.id);

          this.myService.getProductsByParentCategoryId(category.id).subscribe({
            next: (products: any) => {
              if (products && Array.isArray(products)) {
                console.log(`Loaded ${products.length} products for category ${category.name}`);

                // Add category information to each product
                const productsWithCategory = products.map((product: any) => {
                  // If the product doesn't have a categories array, create one
                  if (!product.categories || !Array.isArray(product.categories)) {
                    product.categories = [];
                  }

                  // Add this category to the product's categories if it's not already there
                  const hasCategory = product.categories.some((cat: any) => cat.id === category.id);
                  if (!hasCategory) {
                    product.categories.push({
                      id: category.id,
                      name: category.name
                    });
                  }

                  return product;
                });

                // Log a sample product to understand its structure
                if (productsWithCategory.length > 0) {
                  console.log('Sample product:', productsWithCategory[0].name, 'ID:', productsWithCategory[0].id);
                  console.log('Sample product categories:', productsWithCategory[0].categories);
                  console.log('Sample product info:', productsWithCategory[0].productInfo);
                }

                combinedProducts = combinedProducts.concat(productsWithCategory);
              }
              processedCategories++;

              // When all categories are processed, set the products
              if (processedCategories === parentCategories.length) {
                console.log(`All products loaded: ${combinedProducts.length} products`);

                // Store all products for pagination
                this.allProducts = combinedProducts;

                // Store the original products so we can restore them when clearing filters
                this.originalAllProducts = [...combinedProducts];

                // Apply pagination to show the first page
                const startIndex = 0;
                const endIndex = Math.min(this.itemsPerPage, combinedProducts.length);
                this.productsofSub = combinedProducts.slice(startIndex, endIndex);

                // Calculate total pages
                this.totalPages = Math.ceil(combinedProducts.length / this.itemsPerPage);

                console.log('Total products:', combinedProducts.length);
                console.log('Products on first page:', this.productsofSub.length);
                console.log('Total pages:', this.totalPages);

                // Log the first few products
                if (this.productsofSub && this.productsofSub.length > 0) {
                  console.log('First few products:', this.productsofSub.slice(0, 3).map((p: any) => p.name));
                }
              }
            },
            error: (err) => {
              console.log('Error fetching products for category:', category.name, err);
              processedCategories++;

              // Even if there's an error, check if all categories have been processed
              if (processedCategories === parentCategories.length) {
                console.log(`Finished loading with some errors. Total products: ${combinedProducts.length}`);

                // Store all products for pagination
                this.allProducts = combinedProducts;

                // Store the original products so we can restore them when clearing filters
                this.originalAllProducts = [...combinedProducts];

                // Apply pagination to show the first page
                const startIndex = 0;
                const endIndex = Math.min(this.itemsPerPage, combinedProducts.length);
                this.productsofSub = combinedProducts.slice(startIndex, endIndex);

                // Calculate total pages
                this.totalPages = Math.ceil(combinedProducts.length / this.itemsPerPage);

                console.log('Total products:', combinedProducts.length);
                console.log('Products on first page:', this.productsofSub.length);
                console.log('Total pages:', this.totalPages);
              }
            }
          });
        });
      },
      error: (err) => {
        console.log('Error fetching parent categories:', err);
        this.productsofSub = [];
        this.allProducts = [];
        this.originalAllProducts = [];
      }
    });
  }

  // Load all subcategories for filtering on the All Products page
  loadAllSubcategories() {
    console.log('Loading all subcategories for filtering');

    // Get all categories
    this.categoryService.GetAllCategries().subscribe({
      next: (allCategories: any) => {
        if (allCategories && Array.isArray(allCategories)) {
          console.log('All categories loaded:', allCategories.length);

          // Filter to get only subcategories (those with parentCategoryId)
          const subcategories = allCategories.filter((cat: any) => cat.parentCategoryId);
          console.log('Subcategories (before deduplication):', subcategories.length);

          // Remove duplicates by name
          const uniqueSubcategories = this.removeDuplicateSubcategories(subcategories);
          console.log(`Found ${uniqueSubcategories.length} unique subcategories for filtering`);

          // Store subcategories for filtering
          this.originSubCateories = uniqueSubcategories;
          this.subCategories = this.originSubCateories;

          // For All Products page, we need to load products for each subcategory
          // just like we do for category pages
          if (uniqueSubcategories.length > 0) {
            console.log('Loading products for each subcategory...');
            let processedSubcategories = 0;

            uniqueSubcategories.forEach((subcat: any) => {
              this.myService.getProductsbyCategory(subcat.id).subscribe({
                next: (products: any) => {
                  // Add category information to each product
                  const productsWithCategory = products ? products.map((product: any) => {
                    // If the product doesn't have a categories array, create one
                    if (!product.categories || !Array.isArray(product.categories)) {
                      product.categories = [];
                    }

                    // Add this subcategory to the product's categories if it's not already there
                    const hasCategory = product.categories.some((cat: any) => cat.id === subcat.id);
                    if (!hasCategory) {
                      product.categories.push({
                        id: subcat.id,
                        name: subcat.name
                      });
                    }

                    return product;
                  }) : [];

                  // Assign products to the subcategory
                  subcat.products = productsWithCategory;
                  processedSubcategories++;

                  console.log(`Loaded ${productsWithCategory?.length || 0} products for subcategory ${subcat.name} (${processedSubcategories}/${uniqueSubcategories.length})`);

                  if (processedSubcategories === uniqueSubcategories.length) {
                    console.log('All subcategory products loaded for filtering');

                    // Update originproducts for filtering
                    this.originproducts = this.originSubCateories
                      .map((p: { products: any }) => p.products)
                      .filter((p: any) => !isEmptyObject(p));

                    console.log('Origin products arrays:', this.originproducts.length);
                  }
                },
                error: (err) => {
                  console.log(`Error loading products for subcategory ${subcat.id}:`, err);
                  processedSubcategories++;

                  if (processedSubcategories === uniqueSubcategories.length) {
                    // Even with errors, try to use what we have
                    this.originproducts = this.originSubCateories
                      .map((p: { products: any }) => p.products)
                      .filter((p: any) => !isEmptyObject(p));

                    console.log('Origin products arrays after some errors:', this.originproducts.length);
                  }
                }
              });
            });
          }
        }
      },
      error: (err) => {
        console.log('Error loading all subcategories:', err);
      }
    });
  }

  // Helper method to remove duplicate subcategories
  removeDuplicateSubcategories(subcategories: any[]): any[] {
    const uniqueMap = new Map();

    // Use a Map to keep only unique subcategories by name
    subcategories.forEach((subcat) => {
      if (!uniqueMap.has(subcat.name)) {
        uniqueMap.set(subcat.name, subcat);
      }
    });

    // Convert Map values back to array
    return Array.from(uniqueMap.values());
  }

  // Check if a product is in the wishlist
  isInWishlist(productId: string): boolean {
    return this.wishlistProductIds.includes(productId);
  }

  // Toggle product in wishlist
  toggleWishlist(event: Event, productId: string): void {
    event.stopPropagation(); // Prevent event bubbling

    console.log('Toggling wishlist for product:', productId);

    this.wishListService.toggleProductInWishlist(productId).subscribe(
      () => {
        console.log('Wishlist toggled successfully');
      },
      (error) => {
        console.error('Error toggling wishlist:', error);

        // If there's an authentication error, redirect to login
        if (error.status === 401) {
          this.router.navigate(['/login']);
        }
      }
    );
  }

  getProductsByParentCategory() {
    console.log('Getting products directly by parent category ID:', this.CategoryId);

    // First, get the category name
    this.categoryService.GetAllCategries().subscribe({
      next: (allCategories: any) => {
        const category = allCategories.find((cat: any) => cat.id === this.CategoryId);
        if (category) {
          this.categoryName = category.name;
          console.log('Category name found:', this.categoryName);
        }
      },
      error: (err) => {
        console.log('Error getting category name:', err);
      }
    });

    // Directly use the endpoint to get products by parent category ID
    this.myService.getProductsByParentCategoryId(this.CategoryId).subscribe({
      next: (products) => {
        console.log('Products by parent category response:', products);

        if (products && this.isArray(products) && (products as any[]).length > 0) {
          console.log('SUCCESS: Got products by parent category ID');
          this.productsofSub = products;

          // Also get subcategories for filtering
          this.myService.getSubcategories(this.CategoryId).subscribe({
            next: (subcategories) => {
              this.originSubCateories = subcategories;
              this.subCategories = this.originSubCateories;
            },
            error: (err) => {
              console.log('Error fetching subcategories:', err);
            }
          });
        } else {
          console.log('No products found by parent category ID');
          // Try the general method as fallback
          this.getProductsByCategory();
        }
      },
      error: (err) => {
        console.log('Error getting products by parent category:', err);
        // Try the general method as fallback
        this.getProductsByCategory();
      }
    });
  }

  getProductsByCategory() {
    console.log('Getting products for category ID:', this.CategoryId);

    // Set isAllProducts flag to false since we're on a category page
    this.isAllProducts = false;
    console.log('Is All Products page:', this.isAllProducts);

    // Let's try a direct approach first - get products by category ID
    console.log('Trying direct API call to get products by category ID');
    this.myService.getProductsbyCategory(this.CategoryId).subscribe({
      next: (products) => {
        console.log('Direct API response for products by category:', products);
        if (products && this.isArray(products) && (products as any[]).length > 0) {
          console.log('SUCCESS: Got products directly by category ID');
          this.productsofSub = products;

          // Store all products for pagination
          this.allProducts = products as any[];

          // Store the original products so we can restore them when clearing filters
          this.originalAllProducts = [...this.allProducts];

          // Get category name
          this.categoryService.GetAllCategries().subscribe({
            next: (allCategories: any) => {
              const category = allCategories.find((cat: any) => cat.id === this.CategoryId);
              if (category) {
                this.categoryName = category.name;
                console.log('Category name found:', this.categoryName);

                // Set breadcrumb path for subcategory
                this.breadcrumbPath = `Home / Products / ${this.categoryName}`;
              }
            }
          });
        } else {
          console.log('No products found directly, trying fallback methods');
          this.tryFallbackMethods();
        }
      },
      error: (err) => {
        console.log('Error getting products directly:', err);
        this.tryFallbackMethods();
      }
    });
  }

  tryFallbackMethods() {
    console.log('Trying fallback method 1: Get subcategories and their products');

    // Set isAllProducts flag to false since we're on a category page
    this.isAllProducts = false;
    console.log('Is All Products page:', this.isAllProducts);

    // Try to get subcategories first
    this.myService.getSubcategories(this.CategoryId).subscribe({
      next: (subcategories) => {
        console.log('Subcategories response:', subcategories);
        this.originSubCateories = subcategories;
        this.subCategories = this.originSubCateories;

        if (subcategories && this.isArray(subcategories) && (subcategories as any[]).length > 0) {
          console.log('Found subcategories, extracting products');

          // Extract products from subcategories
          this.originproducts = this.originSubCateories
            .map((p: { products: any }) => p.products)
            .filter((p: any) => !isEmptyObject(p));

          console.log('Extracted products from subcategories:', this.originproducts);

          if (this.originproducts && this.isArray(this.originproducts) && (this.originproducts as any[]).length > 0) {
            this.productsofSub = [].concat(...this.originproducts);
            console.log('SUCCESS: Combined products from subcategories:', this.productsofSub);

            // Store all products for pagination
            this.allProducts = [...this.productsofSub];

            // Store the original products so we can restore them when clearing filters
            this.originalAllProducts = [...this.allProducts];

            // Get category name
            this.categoryService.GetAllCategries().subscribe({
              next: (allCategories: any) => {
                const category = allCategories.find((cat: any) => cat.id === this.CategoryId);
                if (category) {
                  this.categoryName = category.name;
                  console.log('Category name found:', this.categoryName);

                  // Set breadcrumb path for subcategory
                  this.breadcrumbPath = `Home / Products / ${this.categoryName}`;
                }
              }
            });
          } else {
            console.log('No products found in subcategories, trying fallback method 2');
            this.tryFallbackMethod2();
          }
        } else {
          console.log('No subcategories found, trying fallback method 2');
          this.tryFallbackMethod2();
        }
      },
      error: (err) => {
        console.log('Error getting subcategories:', err);
        this.tryFallbackMethod2();
      }
    });
  }

  tryFallbackMethod2() {
    console.log('Trying fallback method 2: Get products by parent category ID');

    // Set isAllProducts flag to false since we're on a category page
    this.isAllProducts = false;
    console.log('Is All Products page:', this.isAllProducts);

    // Try to get products by parent category ID
    this.myService.getProductsByParentCategoryId(this.CategoryId).subscribe({
      next: (products) => {
        console.log('Products by parent category response:', products);

        if (products && this.isArray(products) && (products as any[]).length > 0) {
          console.log('SUCCESS: Got products by parent category ID');
          this.productsofSub = products;

          // Store all products for pagination
          this.allProducts = products as any[];

          // Store the original products so we can restore them when clearing filters
          this.originalAllProducts = [...this.allProducts];

          // Get category name
          this.categoryService.GetAllCategries().subscribe({
            next: (allCategories: any) => {
              const category = allCategories.find((cat: any) => cat.id === this.CategoryId);
              if (category) {
                this.categoryName = category.name;
                console.log('Category name found:', this.categoryName);

                // Set breadcrumb path for subcategory
                this.breadcrumbPath = `Home / Products / ${this.categoryName}`;
              }
            }
          });
        } else {
          console.log('No products found by parent category ID, using original method as last resort');
          this.getProductsByCategoryOriginal();
        }
      },
      error: (err) => {
        console.log('Error getting products by parent category:', err);
        this.getProductsByCategoryOriginal();
      }
    });
  }

  // Original method as fallback
  getProductsByCategoryOriginal() {
    console.log('LAST RESORT: Using original method to get products');

    // Set isAllProducts flag to false since we're on a category page
    this.isAllProducts = false;
    console.log('Is All Products page:', this.isAllProducts);

    // Try to get the category name if we don't have it yet
    if (!this.categoryName) {
      this.categoryService.GetAllCategries().subscribe({
        next: (allCategories: any) => {
          console.log('All categories response:', allCategories);
          const category = allCategories.find((cat: any) => cat.id === this.CategoryId);
          if (category) {
            this.categoryName = category.name;
            this.categoryType = category.parentCategoryId ? 'sub' : 'parent';
            console.log('Category name found in original method:', this.categoryName);

            // Set breadcrumb path for category
            this.breadcrumbPath = `Home / Products / ${this.categoryName}`;
          } else {
            console.log('Category not found in all categories');
          }
        },
        error: (err) => {
          console.log('Error getting all categories:', err);
        }
      });
    }

    console.log('Getting subcategories in original method for ID:', this.CategoryId);
    this.myService.getSubcategories(this.CategoryId).subscribe({
      next: (res) => {
        console.log('Subcategories response in original method:', res);
        this.originSubCateories = res;
        this.subCategories = this.originSubCateories;

        if (this.originSubCateories && this.isArray(this.originSubCateories) && (this.originSubCateories as any[]).length > 0) {
          console.log('Found subcategories in original method, extracting products');
          this.originproducts = this.originSubCateories
            .map((p: { products: any }) => p.products)
            .filter((p: any) => !isEmptyObject(p));

          console.log('Extracted products in original method:', this.originproducts);

          if (this.originproducts && this.isArray(this.originproducts) && (this.originproducts as any[]).length > 0) {
            this.productsofSub = this.originproducts;
            console.log('Processing products in original method');

            const arrrry = [].concat(...this.productsofSub);
            this.productsofSub = arrrry;
            console.log('SUCCESS: Final products in original method:', this.productsofSub);

            // Store all products for pagination
            this.allProducts = [...this.productsofSub];

            // Store the original products so we can restore them when clearing filters
            this.originalAllProducts = [...this.allProducts];

            // Apply pagination
            this.applyPagination();
          } else {
            console.log('No products found in subcategories in original method');
            this.productsofSub = [];
            this.allProducts = [];
            this.originalAllProducts = [];
          }
        } else {
          console.log('No subcategories found in original method');
          this.productsofSub = [];
          this.allProducts = [];
          this.originalAllProducts = [];
        }
      },
      error: (err) => {
        console.log('Error in original method:', err);
        this.productsofSub = [];
        this.allProducts = [];
        this.originalAllProducts = [];
      },
    });
  }

  getProductsbyCategory(subcategoryId: string) {
    this.myService.getProductsbyCategory(subcategoryId).subscribe({
      next: (res) => {
        this.productsofSub = res;
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
    console.log(this.productsofSub);
  }

  /*GetAll() {
    this.myService.getProductsByParentCategoryId(this.CategoryId).subscribe({
      next: (res) => {
        this.productsofSub = res;
        this.originproducts =res;
        console.log("done");
        console.log(this.productsofSub);
        this.FilterCombo();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }*/

  subId(_: any = null) {
    // this.subCategoryId = subId;
    // Method kept for compatibility
  }

  color(col: any) {
    this.Color = col;
  }

  size(si: any) {
    this.Size = si;
  }

  /*Filter() {
    this.myService
      .getProductsFiltered(
        this.CategoryId,
        this.subCategoryId,
        this.Color,
        this.Size
      )
      .subscribe({
        next: (res) => {
          this.productsofSub = res;
          console.log(this.productsofSub);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }*/

  Colors: string[] = [
    'Red',
    'Blue',
    'Green',
    'Yellow',
    'Orange',
    'Purple',
    'Pink',
    'Black',
    'White',
    'Gray',
    ' Brown',
  ];

  Sizes: string[] = ['S', 'M', 'L', 'XL', 'XXL', 'XXXL'];

  // getProducts(page: number) {
  //   this.isLoading = true;
  //   this.myService
  //     .getProductsPaginagtion(page, this.counterPerPage)
  //     .subscribe((data: any) => {
  //       console.log(data);

  //       this.isLoading = false;
  //       this.totalCount = data.totalCount;
  //       this.items = data.items;
  //       console.log(this.items);
  //     });
  // }

  fiterdataBySubCategory(e: any) {
    console.log('Subcategory checkbox changed:', e.target.value, 'Checked:', e.target.checked);

    if (e.target.checked) {
      // Add to filter if checked
      this.filter.subCategories.push(e.target.value);
    } else {
      // Remove from filter if unchecked
      let index = this.filter.subCategories.indexOf(e.target.value);
      if (index !== -1) {
        this.filter.subCategories.splice(index, 1);
      }
    }
    console.log('Updated subcategory filters:', this.filter.subCategories);
  }

  fiterdataByColor(e: any) {
    console.log('Color checkbox changed:', e.target.value, 'Checked:', e.target.checked);

    if (e.target.checked) {
      // Add to filter if checked
      this.filter.color.push(e.target.value);
    } else {
      // Remove from filter if unchecked
      let index = this.filter.color.indexOf(e.target.value);
      if (index !== -1) {
        this.filter.color.splice(index, 1);
      }
    }
    console.log('Updated color filters:', this.filter.color);
  }

  fiterdataBySize(e: any) {
    console.log('Size checkbox changed:', e.target.value, 'Checked:', e.target.checked);
    console.log('Size checkbox element:', e.target.id, e.target.className);

    if (e.target.checked) {
      // Add to filter if checked
      this.filter.size.push(e.target.value);
    } else {
      // Remove from filter if unchecked
      let index = this.filter.size.indexOf(e.target.value);
      if (index !== -1) {
        this.filter.size.splice(index, 1);
      }
    }
    console.log('Updated size filters:', this.filter.size);
  }

  FilterCombo() {
    console.log('FILTER BUTTON CLICKED - ENHANCED IMPLEMENTATION');

    // Reset pagination
    this.currentPage = 1;

    // Check if we're on the All Products page by looking at the URL
    const currentUrl = window.location.href;
    console.log('Current URL:', currentUrl);

    // If the URL contains '/products' but not '/products/' followed by anything else,
    // or if it ends with '/products/', then we're on the All Products page
    const isAllProductsUrl = currentUrl.includes('/products') &&
                            (!currentUrl.match(/\/products\/[^\/]/g) ||
                             currentUrl.endsWith('/products/'));

    // Set the isAllProducts flag based on the URL
    this.isAllProducts = isAllProductsUrl;
    console.log('Is All Products page (based on URL):', this.isAllProducts);

    // Collect all selected filter values
    const selectedSubcategories: string[] = [];
    const selectedColors: string[] = [];
    const selectedSizes: string[] = [];

    // Get all selected checkboxes for subcategories
    const subcategoryCheckboxes = document.querySelectorAll('input[type="checkbox"][id^="category-"]');
    console.log('Found subcategory checkboxes:', subcategoryCheckboxes.length);

    subcategoryCheckboxes.forEach((checkbox: any) => {
      if (checkbox.checked) {
        console.log('Selected subcategory checkbox:', checkbox.id, checkbox.value);
        selectedSubcategories.push(checkbox.value);
      }
    });

    // Get all selected checkboxes for colors
    const colorCheckboxes = document.querySelectorAll('input[type="checkbox"][id^="color-"]');
    console.log('Found color checkboxes:', colorCheckboxes.length);

    colorCheckboxes.forEach((checkbox: any) => {
      if (checkbox.checked) {
        console.log('Selected color checkbox:', checkbox.id, checkbox.value);
        selectedColors.push(checkbox.value);
      }
    });

    // Get all selected checkboxes for sizes
    const sizeCheckboxes = document.querySelectorAll('input.btn-check[id^="size-"]');
    console.log('Found size checkboxes:', sizeCheckboxes.length);

    sizeCheckboxes.forEach((checkbox: any) => {
      if (checkbox.checked) {
        console.log('Selected size checkbox:', checkbox.id, checkbox.value);
        selectedSizes.push(checkbox.value);
      }
    });

    // Get price range values
    const minPriceInput = document.getElementById('minPrice') as HTMLInputElement;
    const maxPriceInput = document.getElementById('maxPrice') as HTMLInputElement;

    const minPrice = minPriceInput?.value ? parseFloat(minPriceInput.value) : 0;
    const maxPrice = maxPriceInput?.value ? parseFloat(maxPriceInput.value) : 999999;

    console.log('Selected filters:');
    console.log('- Subcategories:', selectedSubcategories);
    console.log('- Colors:', selectedColors);
    console.log('- Sizes:', selectedSizes);
    console.log('- Price range:', minPrice, '-', maxPrice);

    // Update the filter object with the collected values
    this.filter = {
      subCategories: selectedSubcategories,
      color: selectedColors,
      size: selectedSizes
    };

    console.log('Current products count before filtering:', this.productsofSub?.length);
    console.log('All products count before filtering:', this.allProducts?.length);
    console.log('Original all products count:', this.originalAllProducts?.length);
    console.log('Origin subcategories count:', this.originSubCateories?.length);

    try {
      // Reset pagination when filtering
      this.currentPage = 1;

      // Get price range values from inputs
      const minPriceInput = document.getElementById('minPrice') as HTMLInputElement;
      const maxPriceInput = document.getElementById('maxPrice') as HTMLInputElement;

      let minPrice = 0;
      let maxPrice = 999999;

      if (minPriceInput && minPriceInput.value) {
        minPrice = parseFloat(minPriceInput.value);
        console.log('Min price set to:', minPrice);
      }

      if (maxPriceInput && maxPriceInput.value) {
        maxPrice = parseFloat(maxPriceInput.value);
        console.log('Max price set to:', maxPrice);
      }

      console.log(`Price range: ${minPrice} - ${maxPrice}`);

      // Track which filters are active
      const activeFilters = {
        subcategory: this.filter.subCategories && this.filter.subCategories.length > 0,
        color: this.filter.color && this.filter.color.length > 0,
        size: this.filter.size && this.filter.size.length > 0,
        price: (minPriceInput && minPriceInput.value) || (maxPriceInput && maxPriceInput.value)
      };

      console.log('Active filters:', activeFilters);
      console.log('Selected subcategories:', this.filter.subCategories);
      console.log('Selected colors:', this.filter.color);
      console.log('Selected sizes:', this.filter.size);

      // If no filters are active, show all products
      if (!activeFilters.subcategory && !activeFilters.color && !activeFilters.size && !activeFilters.price) {
        console.log('No filters active, showing all products');
        if (this.originalAllProducts && this.originalAllProducts.length > 0) {
          console.log('Using original all products');
          this.allProducts = [...this.originalAllProducts];
          this.productsofSub = this.allProducts.slice(0, this.itemsPerPage);
          this.applyPagination();
        } else {
          console.log('No original products, using current all products');
          this.productsofSub = [...this.allProducts];
        }
        return;
      }

      // Log the filter state for debugging
      console.log('Filter state before applying filters:');
      console.log('- Subcategory filter active:', activeFilters.subcategory, this.filter.subCategories);
      console.log('- Color filter active:', activeFilters.color, this.filter.color);
      console.log('- Size filter active:', activeFilters.size, this.filter.size);
      console.log('- Price filter active:', activeFilters.price, `${minPrice} - ${maxPrice}`);

      // For All Products page, use the enhanced filtering approach
      if (this.isAllProducts) {
        console.log('All Products page: Using enhanced filtering approach');

        // Start with all original products
        if (!this.originalAllProducts || this.originalAllProducts.length === 0) {
          console.log('No original products to filter!');
          this.productsofSub = [];
          return;
        }

        console.log('Starting with original products:', this.originalAllProducts.length);

        // Create a copy of the original products to filter
        let filteredProducts = [...this.originalAllProducts];

        // Log a sample product to understand its structure
        if (filteredProducts.length > 0) {
          const sampleProduct = filteredProducts[0];
          console.log('Sample product structure:', JSON.stringify(sampleProduct, null, 2));
        }

        // COMPLETELY REVISED APPROACH: Simplify filtering logic
        console.log('Using completely revised filtering approach');

        // Log the original products before filtering
        console.log('Original products before filtering:', filteredProducts.length);

        // Log the first few original products
        if (filteredProducts.length > 0) {
          console.log('First few original products:');
          filteredProducts.slice(0, 3).forEach((product: any, index: number) => {
            console.log(`Product ${index + 1}:`, product.name, '(ID:', product.id, ')');

            // Log product details to understand its structure
            console.log('- Categories:', product.categories ? product.categories.map((c: any) => `${c.name} (${c.id})`).join(', ') : 'None');
            console.log('- Product Info:', product.productInfo ? product.productInfo.map((i: any) => `Color: ${i.color}, Size: ${i.size}`).join(', ') : 'None');
            console.log('- Price:', product.price, 'Discount:', product.discount);
          });
        }

        // Apply each filter separately and log results after each step

        // 1. Filter by subcategory if needed
        if (activeFilters.subcategory) {
          console.log('Filtering by subcategories:', this.filter.subCategories);

          const beforeCount = filteredProducts.length;
          filteredProducts = filteredProducts.filter((product: any) => {
            // If product has no categories, it can't match
            if (!product.categories || !Array.isArray(product.categories)) {
              return false;
            }

            // Check if any category matches
            return product.categories.some((category: any) =>
              this.filter.subCategories.includes(category.id)
            );
          });

          console.log(`After subcategory filtering: ${filteredProducts.length} products (removed ${beforeCount - filteredProducts.length})`);
        }

        // 2. Filter by color if needed
        if (activeFilters.color) {
          console.log('Filtering by colors:', this.filter.color);

          const beforeCount = filteredProducts.length;
          filteredProducts = filteredProducts.filter((product: any) => {
            // If product has no productInfo, it can't match
            if (!product.productInfo || !Array.isArray(product.productInfo)) {
              return false;
            }

            // Check if any variant matches the color
            return product.productInfo.some((info: any) =>
              this.filter.color.includes(info.color)
            );
          });

          console.log(`After color filtering: ${filteredProducts.length} products (removed ${beforeCount - filteredProducts.length})`);
        }

        // 3. Filter by size if needed
        if (activeFilters.size) {
          console.log('Filtering by sizes:', this.filter.size);

          const beforeCount = filteredProducts.length;
          filteredProducts = filteredProducts.filter((product: any) => {
            // If product has no productInfo, it can't match
            if (!product.productInfo || !Array.isArray(product.productInfo)) {
              return false;
            }

            // Check if any variant matches the size
            return product.productInfo.some((info: any) =>
              this.filter.size.includes(info.size)
            );
          });

          console.log(`After size filtering: ${filteredProducts.length} products (removed ${beforeCount - filteredProducts.length})`);
        }

        // 4. Filter by price if needed
        if (activeFilters.price) {
          console.log('Filtering by price range:', minPrice, '-', maxPrice);

          const beforeCount = filteredProducts.length;
          filteredProducts = filteredProducts.filter((product: any) => {
            // If product has no price, it can't match
            if (!product.price) {
              return false;
            }

            // Calculate actual price with discount
            let actualPrice = parseFloat(product.price);
            if (product.discount && product.discount > 0) {
              actualPrice = actualPrice * (1 - product.discount);
            }

            // Check if price is within range
            return actualPrice >= minPrice && actualPrice <= maxPrice;
          });

          console.log(`After price filtering: ${filteredProducts.length} products (removed ${beforeCount - filteredProducts.length})`);
        }

        // Log the final filtered products
        console.log('Final filtered products:', filteredProducts.length);

        // Log the first few filtered products
        if (filteredProducts.length > 0) {
          console.log('First few filtered products:');
          filteredProducts.slice(0, 3).forEach((product: any, index: number) => {
            console.log(`Product ${index + 1}:`, product.name, '(ID:', product.id, ')');
          });
        } else {
          console.log('No products match the filters!');
        }

        // IMPORTANT: Update the UI with the filtered products

        // Update the filtered products array
        this.allProducts = filteredProducts;

        // Reset pagination to show the first page
        this.currentPage = 1;

        // Apply pagination to show the first page
        const startIndex = 0;
        const endIndex = Math.min(this.itemsPerPage, filteredProducts.length);
        this.productsofSub = filteredProducts.slice(startIndex, endIndex);

        console.log('Final filtered products:', filteredProducts.length);
        console.log('Products on first page:', this.productsofSub.length);

        // Calculate total pages
        this.totalPages = Math.ceil(filteredProducts.length / this.itemsPerPage);
        console.log('Total pages:', this.totalPages);

        // If no products match the filters, ensure productsofSub is empty
        if (filteredProducts.length === 0) {
          console.log('No products match the selected filters');
          this.productsofSub = [];
        }

        // Force Angular change detection by creating a new array
        this.productsofSub = [...this.productsofSub];

        // Log the final state
        console.log('Final state:');
        console.log('- allProducts.length:', this.allProducts.length);
        console.log('- productsofSub.length:', this.productsofSub.length);
        console.log('- totalPages:', this.totalPages);
        console.log('- currentPage:', this.currentPage);
      }
      else {
        console.log('Category page: Using same filtering approach as All Products page');

        // First, prepare all products from subcategories
        if (activeFilters.subcategory) {
          // Get the selected subcategories
          this.subCategories = this.originSubCateories.filter((c: { id: any }) =>
            this.filter.subCategories.includes(c.id)
          );
        } else {
          // If no subcategory filter, use all subcategories
          this.subCategories = this.originSubCateories;
        }

        // Extract all products from subcategories
        let allCategoryProducts = [].concat(
          ...this.subCategories
            .map((c: { products: any }) => c.products)
            .filter((p: any) => !isEmptyObject(p))
        );

        // Log the initial product count
        console.log('Initial category products count:', allCategoryProducts.length);

        // Log the first few original products
        if (allCategoryProducts.length > 0) {
          console.log('First few original category products:');
          allCategoryProducts.slice(0, 3).forEach((product: any, index: number) => {
            console.log(`Product ${index + 1}:`, product.name, '(ID:', product.id, ')');

            // Log product details to understand its structure
            console.log('- Categories:', product.categories ? product.categories.map((c: any) => `${c.name} (${c.id})`).join(', ') : 'None');
            console.log('- Product Info:', product.productInfo ? product.productInfo.map((i: any) => `Color: ${i.color}, Size: ${i.size}`).join(', ') : 'None');
            console.log('- Price:', product.price, 'Discount:', product.discount);
          });
        }

        // Apply each filter separately and log results after each step

        // 1. Filter by color if needed
        if (activeFilters.color) {
          console.log('Filtering by colors:', this.filter.color);

          const beforeCount = allCategoryProducts.length;
          allCategoryProducts = allCategoryProducts.filter((product: any) => {
            // If product has no productInfo, it can't match
            if (!product.productInfo || !Array.isArray(product.productInfo)) {
              return false;
            }

            // Check if any variant matches the color
            return product.productInfo.some((info: any) =>
              this.filter.color.includes(info.color)
            );
          });

          console.log(`After color filtering: ${allCategoryProducts.length} products (removed ${beforeCount - allCategoryProducts.length})`);
        }

        // 2. Filter by size if needed
        if (activeFilters.size) {
          console.log('Filtering by sizes:', this.filter.size);

          const beforeCount = allCategoryProducts.length;
          allCategoryProducts = allCategoryProducts.filter((product: any) => {
            // If product has no productInfo, it can't match
            if (!product.productInfo || !Array.isArray(product.productInfo)) {
              return false;
            }

            // Check if any variant matches the size
            return product.productInfo.some((info: any) =>
              this.filter.size.includes(info.size)
            );
          });

          console.log(`After size filtering: ${allCategoryProducts.length} products (removed ${beforeCount - allCategoryProducts.length})`);
        }

        // 3. Filter by price if needed
        if (activeFilters.price) {
          console.log('Filtering by price range:', minPrice, '-', maxPrice);

          const beforeCount = allCategoryProducts.length;
          allCategoryProducts = allCategoryProducts.filter((product: any) => {
            // If product has no price, it can't match
            if (!product.price) {
              return false;
            }

            // Calculate actual price with discount
            let actualPrice = parseFloat(product.price);
            if (product.discount && product.discount > 0) {
              actualPrice = actualPrice * (1 - product.discount);
            }

            // Check if price is within range
            return actualPrice >= minPrice && actualPrice <= maxPrice;
          });

          console.log(`After price filtering: ${allCategoryProducts.length} products (removed ${beforeCount - allCategoryProducts.length})`);
        }

        // Log the final filtered products
        console.log('Final filtered category products:', allCategoryProducts.length);

        // Log the first few filtered products
        if (allCategoryProducts.length > 0) {
          console.log('First few filtered category products:');
          allCategoryProducts.slice(0, 3).forEach((product: any, index: number) => {
            console.log(`Product ${index + 1}:`, product.name, '(ID:', product.id, ')');
          });
        } else {
          console.log('No category products match the filters!');
        }

        // Update the products to display
        this.productsofSub = allCategoryProducts;

        // Also update allProducts for pagination
        this.allProducts = allCategoryProducts;

        // Reset pagination to show the first page
        this.currentPage = 1;

        // Calculate total pages
        this.totalPages = Math.ceil(allCategoryProducts.length / this.itemsPerPage);

        // Force Angular change detection by creating a new array
        this.productsofSub = [...this.productsofSub];

        // Log the final state
        console.log('Final state for category page:');
        console.log('- allProducts.length:', this.allProducts.length);
        console.log('- productsofSub.length:', this.productsofSub.length);
        console.log('- totalPages:', this.totalPages);
        console.log('- currentPage:', this.currentPage);
      }
    } catch (error) {
      console.error('Error in FilterCombo:', error);
    }
  }



  /**
   * Apply filters when the Apply Filters button is clicked
   */
  applyFilters() {
    console.log('Apply Filters button clicked - DIRECT IMPLEMENTATION');

    // Reset pagination
    this.currentPage = 1;

    // Collect all selected filter values
    const selectedSubcategories: string[] = [];
    const selectedColors: string[] = [];
    const selectedSizes: string[] = [];

    // Get all selected checkboxes for subcategories
    const subcategoryCheckboxes = document.querySelectorAll('input[type="checkbox"][id^="category-"]');
    console.log('Found subcategory checkboxes:', subcategoryCheckboxes.length);

    subcategoryCheckboxes.forEach((checkbox: any) => {
      if (checkbox.checked) {
        console.log('Selected subcategory checkbox:', checkbox.id, checkbox.value);
        selectedSubcategories.push(checkbox.value);
      }
    });

    // Get all selected checkboxes for colors
    const colorCheckboxes = document.querySelectorAll('input[type="checkbox"][id^="color-"]');
    console.log('Found color checkboxes:', colorCheckboxes.length);

    colorCheckboxes.forEach((checkbox: any) => {
      if (checkbox.checked) {
        console.log('Selected color checkbox:', checkbox.id, checkbox.value);
        selectedColors.push(checkbox.value);
      }
    });

    // Get all selected checkboxes for sizes
    const sizeCheckboxes = document.querySelectorAll('input.btn-check[id^="size-"]');
    console.log('Found size checkboxes:', sizeCheckboxes.length);

    sizeCheckboxes.forEach((checkbox: any) => {
      if (checkbox.checked) {
        console.log('Selected size checkbox:', checkbox.id, checkbox.value);
        selectedSizes.push(checkbox.value);
      }
    });

    // Get price range values
    const minPriceInput = document.getElementById('minPrice') as HTMLInputElement;
    const maxPriceInput = document.getElementById('maxPrice') as HTMLInputElement;

    const minPrice = minPriceInput?.value ? parseFloat(minPriceInput.value) : 0;
    const maxPrice = maxPriceInput?.value ? parseFloat(maxPriceInput.value) : 999999;

    console.log('Selected filters:');
    console.log('- Subcategories:', selectedSubcategories);
    console.log('- Colors:', selectedColors);
    console.log('- Sizes:', selectedSizes);
    console.log('- Price range:', minPrice, '-', maxPrice);

    // Check if any filters are active
    const hasSubcategoryFilter = selectedSubcategories.length > 0;
    const hasColorFilter = selectedColors.length > 0;
    const hasSizeFilter = selectedSizes.length > 0;
    const hasPriceFilter = minPriceInput?.value || maxPriceInput?.value;

    // If no filters are active, show all products
    if (!hasSubcategoryFilter && !hasColorFilter && !hasSizeFilter && !hasPriceFilter) {
      console.log('No filters active, showing all products');
      if (this.originalAllProducts && this.originalAllProducts.length > 0) {
        this.allProducts = [...this.originalAllProducts];

        // Apply pagination to show the first page
        const startIndex = 0;
        const endIndex = Math.min(this.itemsPerPage, this.originalAllProducts.length);
        this.productsofSub = this.originalAllProducts.slice(startIndex, endIndex);

        // Calculate total pages
        this.totalPages = Math.ceil(this.originalAllProducts.length / this.itemsPerPage);

        console.log('Showing all products:', this.productsofSub.length);
        console.log('Total pages:', this.totalPages);
      }
      return;
    }

    // Start with all original products
    if (!this.originalAllProducts || this.originalAllProducts.length === 0) {
      console.log('No original products to filter!');
      this.productsofSub = [];
      return;
    }

    console.log('Starting with original products:', this.originalAllProducts.length);

    // Create a copy of the original products to filter
    let filteredProducts = [...this.originalAllProducts];

    // Log a sample product to understand its structure
    if (filteredProducts.length > 0) {
      const sampleProduct = filteredProducts[0];
      console.log('Sample product structure:', JSON.stringify(sampleProduct, null, 2));
    }

    // DIRECT APPROACH: Apply all filters at once
    filteredProducts = filteredProducts.filter((product: any) => {
      // Track which filters this product passes
      const passesFilters = {
        subcategory: !hasSubcategoryFilter, // Default to true if no subcategory filter
        color: !hasColorFilter,             // Default to true if no color filter
        size: !hasSizeFilter,               // Default to true if no size filter
        price: !hasPriceFilter              // Default to true if no price filter
      };

      // Check subcategory filter
      if (hasSubcategoryFilter) {
        if (product.categories && Array.isArray(product.categories)) {
          // Check if any of the product's categories match the selected subcategories
          passesFilters.subcategory = product.categories.some((category: any) =>
            selectedSubcategories.includes(category.id)
          );
        }
      }

      // Check color filter
      if (hasColorFilter) {
        if (product.productInfo && Array.isArray(product.productInfo)) {
          // Check if any of the product's colors match the selected colors
          passesFilters.color = product.productInfo.some((info: any) =>
            selectedColors.includes(info.color)
          );
        }
      }

      // Check size filter
      if (hasSizeFilter) {
        if (product.productInfo && Array.isArray(product.productInfo)) {
          // Check if any of the product's sizes match the selected sizes
          passesFilters.size = product.productInfo.some((info: any) =>
            selectedSizes.includes(info.size)
          );
        }
      }

      // Check price filter
      if (hasPriceFilter) {
        if (product.price) {
          // Calculate actual price with discount
          let actualPrice = parseFloat(product.price);
          if (product.discount && product.discount > 0) {
            actualPrice = actualPrice * (1 - product.discount);
          }

          // Check if price is within range
          passesFilters.price = actualPrice >= minPrice && actualPrice <= maxPrice;
        }
      }

      // Log detailed filter results for the first few products
      if (filteredProducts.indexOf(product) < 3) {
        console.log(`Filter results for product "${product.name}" (ID: ${product.id}):`);
        console.log('- Passes subcategory filter:', passesFilters.subcategory);
        console.log('- Passes color filter:', passesFilters.color);
        console.log('- Passes size filter:', passesFilters.size);
        console.log('- Passes price filter:', passesFilters.price);
      }

      // Product passes if it passes all active filters
      return passesFilters.subcategory && passesFilters.color && passesFilters.size && passesFilters.price;
    });

    console.log('After filtering:', filteredProducts.length);

    // Log the first few filtered products
    if (filteredProducts.length > 0) {
      console.log('First few filtered products:');
      filteredProducts.slice(0, 3).forEach((product: any, index: number) => {
        console.log(`Product ${index + 1}:`, product.name, '(ID:', product.id, ')');
      });
    }

    // Update the filtered products
    this.allProducts = filteredProducts;

    // Apply pagination to show the first page
    const startIndex = 0;
    const endIndex = Math.min(this.itemsPerPage, filteredProducts.length);
    this.productsofSub = filteredProducts.slice(startIndex, endIndex);

    console.log('Final filtered products:', filteredProducts.length);
    console.log('Products on first page:', this.productsofSub.length);

    // Calculate total pages
    this.totalPages = Math.ceil(filteredProducts.length / this.itemsPerPage);
    console.log('Total pages:', this.totalPages);

    // If no products match the filters, show a message
    if (filteredProducts.length === 0) {
      console.log('No products match the selected filters');
      this.productsofSub = [];
    }
  }

  clear(_: any = null) {
    console.log("Clearing all filters - COMPLETELY REVISED IMPLEMENTATION");

    // Check if we're in search mode
    if (this.isSearching) {
      console.log("Clearing search results");

      // Navigate back to products page without search query
      this.router.navigate(['/products']);
      return;
    }

    // Reset pagination
    this.currentPage = 1;

    // Reset filters
    this.filter = {
      subCategories: [],
      color: [],
      size: [],
    };

    // Uncheck all checkboxes (including regular checkboxes and btn-check class checkboxes)
    const regularCheckboxes = document.querySelectorAll('input[type="checkbox"]');
    const btnCheckboxes = document.querySelectorAll('input.btn-check');

    console.log('Unchecking regular checkboxes:', regularCheckboxes.length);
    regularCheckboxes.forEach((checkbox: any) => {
      checkbox.checked = false;
    });

    console.log('Unchecking btn-check checkboxes:', btnCheckboxes.length);
    btnCheckboxes.forEach((checkbox: any) => {
      checkbox.checked = false;
    });

    // Reset price range inputs
    const minPriceInput = document.getElementById('minPrice') as HTMLInputElement;
    const maxPriceInput = document.getElementById('maxPrice') as HTMLInputElement;
    if (minPriceInput) minPriceInput.value = '';
    if (maxPriceInput) maxPriceInput.value = '';

    // Check if we're on the All Products page
    const currentUrl = window.location.href;
    const isAllProductsUrl = currentUrl.includes('/products') &&
                            (!currentUrl.match(/\/products\/[^\/]/g) ||
                             currentUrl.endsWith('/products/'));
    this.isAllProducts = isAllProductsUrl;
    console.log('Is All Products page (based on URL):', this.isAllProducts);

    try {
      // Restore original products
      if (this.originalAllProducts && this.originalAllProducts.length > 0) {
        console.log('Restoring original products:', this.originalAllProducts.length);

        // Update allProducts with the original products
        this.allProducts = [...this.originalAllProducts];

        // Apply pagination to show the first page
        const startIndex = 0;
        const endIndex = Math.min(this.itemsPerPage, this.originalAllProducts.length);
        this.productsofSub = this.originalAllProducts.slice(startIndex, endIndex);

        // Calculate total pages
        this.totalPages = Math.ceil(this.originalAllProducts.length / this.itemsPerPage);

        console.log('Showing all products:', this.productsofSub.length);
        console.log('Total pages:', this.totalPages);

        // Log the first few products to verify
        if (this.productsofSub.length > 0) {
          console.log('First few products after clearing filters:');
          this.productsofSub.slice(0, 3).forEach((product: any, index: number) => {
            console.log(`Product ${index + 1}:`, product.name);
          });
        }

        // Force Angular change detection by creating a new array
        this.productsofSub = [...this.productsofSub];
      } else {
        console.log('No original products found, reloading all products');
        if (this.isAllProducts) {
          this.getAllProducts();
        } else if (this.CategoryId) {
          // For category pages, reload products for the current category
          this.getProductsByCategory();
        } else {
          // Fallback to loading all products
          this.getAllProducts();
        }
      }

      // Log the final state
      console.log('Final state after clearing filters:');
      console.log('- allProducts.length:', this.allProducts?.length || 0);
      console.log('- productsofSub.length:', this.productsofSub?.length || 0);
      console.log('- totalPages:', this.totalPages);
      console.log('- currentPage:', this.currentPage);

      console.log("Filters cleared, showing all products");
    } catch (error) {
      console.error('Error in clear method:', error);
      // Fallback to loading all products
      this.getAllProducts();
    }
  }

  // Helper method to check if an object is an array (for use in template)
  isArray(obj: any): boolean {
    return Array.isArray(obj);
  }



  // Pagination methods

  /**
   * Change the current page and update displayed products
   */
  changePage(page: number): void {
    // Validate page number
    if (page < 1 || (this.totalPages > 0 && page > this.totalPages)) {
      return;
    }

    // Update current page
    this.currentPage = page;

    // Apply pagination to products
    this.applyPagination();

    // Check if we need to re-apply sorting
    const savedSortOption = localStorage.getItem('productSortOption');
    if (savedSortOption && savedSortOption !== 'featured') {
      // Re-apply the current sort option to maintain sorting across pages
      const sortSelect = document.querySelector('.sort-options select') as HTMLSelectElement | null;
      if (sortSelect) {
        sortSelect.value = savedSortOption;
      }
    }

    // Scroll to top of products section
    const productsSection = document.querySelector('.products-section');
    if (productsSection) {
      productsSection.scrollIntoView({ behavior: 'smooth' });
    }
  }

  /**
   * Apply pagination to the products
   */
  applyPagination(): void {
    console.log('Applying pagination - NEW IMPLEMENTATION');

    // If we don't have any products, there's nothing to paginate
    if (!this.allProducts || this.allProducts.length === 0) {
      console.log('No products to paginate');
      this.productsofSub = [];
      this.totalPages = 1;
      return;
    }

    // Calculate total pages
    this.totalPages = Math.ceil(this.allProducts.length / this.itemsPerPage);
    console.log('Total products:', this.allProducts.length);
    console.log('Items per page:', this.itemsPerPage);
    console.log('Total pages:', this.totalPages);

    // Ensure current page is valid
    if (this.currentPage < 1) {
      this.currentPage = 1;
    } else if (this.currentPage > this.totalPages) {
      this.currentPage = this.totalPages;
    }

    // Calculate start and end indices for current page
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = Math.min(startIndex + this.itemsPerPage, this.allProducts.length);

    // Get products for current page
    this.productsofSub = this.allProducts.slice(startIndex, endIndex);

    console.log(`Showing products ${startIndex + 1} to ${endIndex} (Page ${this.currentPage} of ${this.totalPages})`);
    console.log('Products on current page:', this.productsofSub.length);

    // Log the first product on the current page
    if (this.productsofSub.length > 0) {
      console.log('First product on current page:', this.productsofSub[0].name);
    } else {
      console.log('No products on current page!');
    }
  }

  /**
   * Get an array of page numbers for pagination
   * Shows a limited number of pages with ellipsis for better UX
   */
  getPageNumbers(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5; // Maximum number of page buttons to show

    // Calculate total pages (this is now done in applyPagination, but we'll keep it here as a fallback)
    if (!this.totalPages && this.allProducts) {
      this.totalPages = Math.ceil(this.allProducts.length / this.itemsPerPage);
    } else if (!this.totalPages) {
      this.totalPages = 1;
    }

    // If we have a small number of pages, show all of them
    if (this.totalPages <= maxPagesToShow) {
      for (let i = 1; i <= this.totalPages; i++) {
        pages.push(i);
      }
    } else {
      // We have many pages, so we need to be selective

      // Always include first page
      pages.push(1);

      // Calculate start and end of the shown pages
      let startPage = Math.max(2, this.currentPage - Math.floor(maxPagesToShow / 2));
      let endPage = Math.min(this.totalPages - 1, startPage + maxPagesToShow - 3);

      // Adjust if we're near the end
      if (endPage >= this.totalPages - 1) {
        startPage = Math.max(2, this.totalPages - maxPagesToShow + 2);
      }

      // Add ellipsis if needed
      if (startPage > 2) {
        pages.push(-1); // Use -1 to represent ellipsis
      }

      // Add the middle pages
      for (let i = startPage; i <= endPage; i++) {
        pages.push(i);
      }

      // Add ellipsis if needed
      if (endPage < this.totalPages - 1) {
        pages.push(-2); // Use -2 to represent ellipsis
      }

      // Always include last page
      pages.push(this.totalPages);
    }

    return pages;
  }

  /**
   * Handle sort dropdown change event
   * @param event The change event from the sort dropdown
   */
  onSortChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    if (select && select.value) {
      this.sortProducts(select.value);
    }
  }

  /**
   * Toggle between grid and list view modes
   * @param mode The view mode to set ('grid' or 'list')
   */
  toggleView(mode: string): void {
    console.log('Toggling view mode to:', mode);

    // Only update if the mode is different
    if (this.viewMode !== mode) {
      this.viewMode = mode;

      // Save the preference to localStorage
      localStorage.setItem('productViewMode', mode);

      console.log('View mode updated to:', mode);
    }
  }

  sortProducts(sortOption: string) {
    console.log('Sorting products by:', sortOption);

    if (!this.allProducts || this.allProducts.length === 0) {
      console.log('No products to sort');
      return;
    }

    // Store the sort option to remember it when changing pages
    localStorage.setItem('productSortOption', sortOption);

    // For featured option, restore original order
    if (sortOption === 'featured') {
      console.log('Restoring original order (featured)');

      // If we have original products, use them
      if (this.originalAllProducts && this.originalAllProducts.length > 0) {
        this.allProducts = [...this.originalAllProducts];
      }

      // Apply pagination to show the current page
      this.applyPagination();
      return;
    }

    // Create a copy of all products to sort
    const sortedProducts = [...this.allProducts];

    switch (sortOption) {
      case 'price-low':
        // Sort by price low to high
        sortedProducts.sort((a, b) => {
          const priceA = a.price * (1 - (a.discount || 0));
          const priceB = b.price * (1 - (b.discount || 0));
          return priceA - priceB;
        });
        break;

      case 'price-high':
        // Sort by price high to low
        sortedProducts.sort((a, b) => {
          const priceA = a.price * (1 - (a.discount || 0));
          const priceB = b.price * (1 - (b.discount || 0));
          return priceB - priceA;
        });
        break;

      case 'newest':
        // Since there's no date field in the product model, we'll use the product ID as a proxy for creation time
        console.log('Sorting by newest first');

        // Log a sample of products before sorting
        if (sortedProducts.length > 0) {
          console.log('Sample product before sorting:', sortedProducts[0]);
          console.log('Product ID example:', sortedProducts[0].id);
        }

        // Assuming that products with higher/newer IDs are newer products
        sortedProducts.sort((a, b) => {
          // First try to use product ID as a proxy for creation time
          if (a.id && b.id) {
            // Convert GUIDs to strings and compare them
            // This works because GUIDs often have a timestamp component
            return b.id.localeCompare(a.id);
          }

          // If no ID is available, try to use the index in the original array
          // This assumes that newer products are added later in the array
          const indexA = this.originalAllProducts.findIndex(p => p.id === a.id);
          const indexB = this.originalAllProducts.findIndex(p => p.id === b.id);

          // Higher index means newer product
          return indexB - indexA;
        });

        // Log a sample of products after sorting
        if (sortedProducts.length > 0) {
          console.log('Sample product after sorting:', sortedProducts[0]);
          console.log('First few products after sorting by newest:');
          sortedProducts.slice(0, 3).forEach((product, index) => {
            console.log(`${index + 1}. ${product.name} (ID: ${product.id})`);
          });
        }
        break;

      case 'rating':
        // Sort by rating (highest first)
        console.log('Sorting by top rated');

        // Log a sample of products before sorting
        if (sortedProducts.length > 0) {
          console.log('Sample product before sorting:', sortedProducts[0]);
          console.log('Product rating example:', sortedProducts[0].rate);
        }

        sortedProducts.sort((a, b) => {
          // Get the rating values, defaulting to 0 if not present
          // Handle both lowercase 'rate' and uppercase 'Rate' properties
          const ratingA = a.rate !== undefined ? a.rate : (a.Rate !== undefined ? a.Rate : 0);
          const ratingB = b.rate !== undefined ? b.rate : (b.Rate !== undefined ? b.Rate : 0);

          // Sort in descending order (highest first)
          return ratingB - ratingA;
        });

        // Log a sample of products after sorting
        if (sortedProducts.length > 0) {
          console.log('Sample product after sorting:', sortedProducts[0]);
          console.log('First few products after sorting by top rated:');
          sortedProducts.slice(0, 3).forEach((product, index) => {
            const rating = product.rate !== undefined ? product.rate : (product.Rate !== undefined ? product.Rate : 0);
            console.log(`${index + 1}. ${product.name} (Rating: ${rating})`);
          });
        }
        break;

      case 'name-az':
        // Sort by name A-Z
        sortedProducts.sort((a, b) => {
          return a.name.localeCompare(b.name);
        });
        break;

      case 'name-za':
        // Sort by name Z-A
        sortedProducts.sort((a, b) => {
          return b.name.localeCompare(a.name);
        });
        break;

      default:
        // Default sorting (featured) - no sorting needed
        console.log('Using default sorting (featured)');
        break;
    }

    // Update the allProducts array with the sorted products
    this.allProducts = sortedProducts;

    // Apply pagination to show the current page with sorted products
    this.applyPagination();

    console.log('Products sorted successfully');
  }
}
