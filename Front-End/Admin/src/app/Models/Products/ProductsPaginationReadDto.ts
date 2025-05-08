import ProductsReadDto from './ProductsReadDto';

export default class ProductsPaginationReadDto {
  items: ProductsReadDto[] = [];
  totalCount: number = 0;
}
