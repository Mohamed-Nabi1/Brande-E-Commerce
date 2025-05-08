# Admin Frontend API Endpoints

This document contains all the API endpoints used in the Brande E-commerce admin frontend application. The base URL for all endpoints is: `https://localhost:44341/api/`

## Authentication & Security

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Security/Register` | POST | Register a new customer/admin | `CustomerService` |
| `Security/Login` | POST | Login and get authentication token | `CustomerService` |
| `Test/test` | GET | Test endpoint for authentication | `CustomerService` |

## Customer Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Customer/{CustomerId}` | GET | Get customer information by ID | `CustomerService` |
| `Customer/RestPassword` | PATCH | Reset customer password | `CustomerService` |

## Category Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Category` | GET | Get all categories | `CategoriesService`, `ProductsService` |
| `Category/ParentCategories` | GET | Get all parent categories | `CategoriesService`, `ProductsService` |
| `Category/subCategories/{parentCategoryId}` | GET | Get subcategories for a parent category | `CategoriesService`, `ProductsService` |
| `Category/Add` | POST | Add a new category | `CategoriesService` |
| `Category/PrdouctsForCategory/{subCategoryId}` | GET | Get products for a specific subcategory | `ProductsService` |
| `Category/PrdouctByParentCategory/{parentCategoryId}` | GET | Get products by parent category | `ProductsService` |

## Product Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Products` | GET | Get all products | `ProductsService` |
| `Products/Pagination/{page}/{counterPerPage}` | GET | Get paginated products | `ProductsService` |
| `Products/UniqueProducts` | GET | Get unique products | `ProductsService` |
| `Products/Add` | POST | Add a new product | `ProductsService` |
| `Products/Update/{ProductId}` | GET | Get product by ID for update | `ProductsService` |
| `Products/Update` | PUT | Update a product | `ProductsService` |
| `Products/Images` | POST | Upload product images | `ProductsService` |
| `Products/Delete/{productId}` | DELETE | Delete a product | `ProductsService` |

## Order Management

| Endpoint | Method | Description | Location in Code |
|----------|--------|-------------|-----------------|
| `Orders` | PUT | Update order status | `OrderService` |
| `Orders/OrdersCutomerName` | GET | Get all orders with customer names | `OrderService` |

## Notes

1. The application uses JWT token authentication, which is handled by interceptors that add the token to all requests.
2. The base URL for all API endpoints is `https://localhost:44341/api/`.
3. Most endpoints require authentication, which is managed through the token stored in localStorage.
4. The admin frontend has access to administrative functions like adding/updating products and categories, viewing all customers, and managing orders.
5. Admin users can update order status (e.g., changing from "Pending" to "Delivered" or payment status from "Unpaid" to "Paid").
