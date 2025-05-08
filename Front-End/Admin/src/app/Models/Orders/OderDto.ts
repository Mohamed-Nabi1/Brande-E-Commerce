export interface OrderDto {
  id: string;
  orderDate: string;
  paymentStatus: string;
  paymentMethod: string;
  orderStatus: string;
  discount: number;
  arrivalDate: string;
  street: string;
  city: string;
  country: string;
  customerName: string;
  totalPrice: number;
}

export interface OrderDetails {
  id: string;
  orderData: string;
  paymentStatus: number;
  paymentMethod: number;
  orderStatus: number;
  discount: number;
  arrivalDate: string;
  street: string;
  city: string;
  phoneNumber: string;
  country: number;
  customerId: string;
  customerFname: string;
  customerMname: string;
  customerLname: string;
  orderProducts: OrderProduct[];
  totalPrice: number;
}

export interface OrderProduct {
  id: string;
  name: string;
  description: string;
  price: number;
  discount: number;
  productCount: number;
  color?: string;
  size?: string;
}

// For status mappings
export enum PaymentStatus {
  Unpaid = 0,
  Paid = 1
}

export enum OrderStatus {
  Pending = 0,
  Delivered = 1
}