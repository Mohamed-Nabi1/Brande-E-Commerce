# Customer Frontend API Endpoints

This document contains all the API endpoints used in the Brande E-commerce customer frontend application. The base URL for all endpoints is: `https://localhost:7052/api/`

## Authentication & Security

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Security/Register` | POST | Register a new customer | `CustomerService` |
| `Security/Login` | POST | Login a customer and get authentication token | `CustomerService` |
| `Test/test` | GET | Test endpoint for authentication | `CustomerService` |

## Customer Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Customer/GetByOne` | GET | Get current customer information | `CustomerService`, `CheckOutService` |
| `Customer` | PATCH | Update customer information | `CustomerService` |
| `Customer/RestPassword` | PATCH | Reset customer password | `CustomerService` |

## Category Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Category` | GET | Get all categories | `CategoryService` |
| `Category/ParentCategories` | GET | Get all parent categories | `CategoryService`, `ProductsService` |
| `Category/subCategories/{id}` | GET | Get subcategories for a parent category | `CategoryService`, `ProductsService` |
| `Category/CategoryUnique` | GET | Get unique subcategories | `CategoryService` |
| `Category/PrdouctsForCategory/{subCategoryId}` | GET | Get products for a specific subcategory | `ProductsService` |
| `Category/PrdouctByParentCategory/{parentCategoryId}` | GET | Get products by parent category | `ProductsService` |

## Product Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Products` | GET | Get all products | `ProductsService` |
| `Products/Pagination/{page}/{counterPerPage}` | GET | Get paginated products | `ProductsService` |
| `Products/ColorDistinct/{id}` | GET | Get product details with distinct colors | `ProductDetailsService` |
| `Products/UniqueProducts` | GET | Get unique products | `ProductsService` |
| `product/Details/{productId}` | GET | Get product details | `CartService` |

## Cart Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Cart/CartProducts` | GET | Get cart products for current customer | `CartService`, `CheckOutService` |
| `Cart/AddCart` | POST | Add product to cart | `ProductDetailsService` |
| `Cart/DeletePrdouctFromCart` | DELETE | Delete product from cart | `CartService` |
| `Cart` | DELETE | Clear entire cart | `CartService`, `CheckOutService` |
| `Cart` | PUT | Update cart product | `CartService` |
| `Cart/Increase` | PATCH | Increase product quantity in cart | `CartService` |
| `Cart/Decrease` | PATCH | Decrease product quantity in cart | `CartService` |

## Wishlist Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `WishLists` | GET | Get wishlist for current customer | `WishListService` |
| `WishLists/add` | PATCH | Add product to wishlist | `WishListService` |
| `WishLists/delete` | PATCH | Remove product from wishlist | `WishListService` |

## Order Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Orders` | POST | Place a new order | `CheckOutService` |
| `Orders/ByCustomer` | GET | Get orders for current customer | `OrderDetailsService` |

## Filtering

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Filter` | GET | Filter products by parent category, subcategory, color, and size | `ProductsService` |

## Notes

1. The application uses JWT token authentication, which is handled by the `AuthenticationInterceptor` that adds the token to all requests.
2. The base URL for all API endpoints is `https://localhost:7052/api/`.
3. Most endpoints require authentication, which is managed through the token stored in localStorage.
4. The customer frontend focuses on shopping functionality including browsing products, managing cart and wishlist, and placing orders.
