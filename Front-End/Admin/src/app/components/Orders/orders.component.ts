import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/Orders/order.service';
import { OrderDto, OrderDetails, OrderProduct, PaymentStatus, OrderStatus } from 'src/app/Models/Orders/OderDto';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  constructor(private readonly orderService: OrderService) { }

  allOrders: OrderDto[] = [];
  filteredOrders: OrderDto[] = [];
  selectedOrder: OrderDetails | null = null;
  searchInput: string = '';
  isLoading: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';
  
  summaryCards = [
    { title: 'Total Orders', value: 0, icon: 'bi-box', bg: 'bg-primary' },
    { title: 'Total Sales', value: 0, icon: 'bi-wallet', bg: 'bg-success' },
    { title: 'Paid Orders', value: 0, icon: 'bi-check-circle', bg: 'bg-info' },
    { title: 'Unpaid Orders', value: 0, icon: 'bi-clock', bg: 'bg-warning' }
  ];

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.orderService.getAllOrders().subscribe({
      next: (data: OrderDto[]) => {
        this.allOrders = data;
        this.filteredOrders = [...this.allOrders];
        this.updateSummaryCards();
        this.isLoading = false;
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = 'Failed to load orders. Please try again.';
        this.isLoading = false;
      }
    });
  }

  updateSummaryCards(): void {
    const totalSales = this.allOrders.reduce((sum, order) => sum + (order.totalPrice || 0), 0);
    
    this.summaryCards = [
      { title: 'Total Orders', value: this.allOrders.length, icon: 'bi-box', bg: 'bg-primary' },
      { title: 'Total Sales', value: totalSales, icon: 'bi-wallet', bg: 'bg-success' },
      { title: 'Paid Orders', value: this.paidOrders.length, icon: 'bi-check-circle', bg: 'bg-info' },
      { title: 'Unpaid Orders', value: this.unpaidOrders.length, icon: 'bi-clock', bg: 'bg-warning' }
    ];
  }

  filterOrders(): void {
    if (this.searchInput) {
      const searchTerm = this.searchInput.toLowerCase();
      this.filteredOrders = this.allOrders.filter(order =>
        (order.customerName?.toLowerCase().includes(searchTerm) ||
        order.id?.toLowerCase().includes(searchTerm) ||
        order.city?.toLowerCase().includes(searchTerm))
      );
    } else {
      this.filteredOrders = [...this.allOrders];
    }
  }

  viewOrderDetails(id: string): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.orderService.getOrderDetails(id).subscribe({
      next: (order: OrderDetails) => {
        this.selectedOrder = order;
        this.isLoading = false;
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = 'Failed to load order details.';
        this.isLoading = false;
      }
    });
  }

  closeDetails(): void {
    this.selectedOrder = null;
  }

  changeToPaid(order: OrderDto | OrderDetails): void {
    debugger;
    const updateData = {
      ...order,  // Spread first to get all properties
      paymentStatus: 'Paid',  // Then override specific properties
      orderStatus: 'Delivered'
    };

    this.isLoading = true;
    this.orderService.updateOrder(updateData).subscribe({
      next: () => {
        this.successMessage = 'Order marked as paid and delivered successfully!';
        this.loadOrders();
        if (this.selectedOrder) {
          this.viewOrderDetails(order.id); // Refresh details view
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = 'Failed to update order status.';
        this.isLoading = false;
      }
    });
  }

  calculateProductTotal(product: OrderProduct): number {
    const price = product.price || 0;
    const discount = product.discount || 0;
    const quantity = product.productCount || 1;
    return (price * (1 - discount / 100)) * quantity;
  }

  calculateOrderTotal(products: OrderProduct[]): number {
    return products.reduce((total, product) => total + this.calculateProductTotal(product), 0);
  }

  formatCurrency(amount: number | undefined): string {
    if (amount === undefined || amount === null) return 'EGP0.00';
    return 'EGP' + amount.toFixed(2);
  }

  getPaymentStatusString(status: number): string {
    return this.orderService.getPaymentStatusString(status);
  }

  getOrderStatusString(status: number): string {
    return this.orderService.getOrderStatusString(status);
  }

  get paidOrders() {
    return this.allOrders.filter(order => order.paymentStatus === 'Paid');
  }

  get unpaidOrders() {
    return this.allOrders.filter(order => order.paymentStatus === 'Unpaid');
  }
}