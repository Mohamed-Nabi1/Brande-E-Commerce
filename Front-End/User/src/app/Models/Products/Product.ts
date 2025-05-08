export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  discount: number;
  rate: number;
  ProductImages: string;

}


export interface ProductDetails 
{
  id: string;
  categoryId: string;
  name: string;
  description: string;
  price: number;
  discount: number;
  rate: number;
  productImages: {
    imageURL: string;
  }[];
  productInfo: {
    color: string;
    size: string;
    quantity: number;
  }[];
}