import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from 'src/app/Models/Products/Product';
import ProductsReadDto from 'src/app/Models/Products/ProductsReadDto';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  constructor(private http: HttpClient) { }
  BASE_URL = 'https://localhost:44341/api/'
  getProductsPaginagtion(
    page: number,
    counterPerPage: number
  ): Observable<ProductsReadDto> {
    return this.http.get<ProductsReadDto>(
      `https://localhost:44341/api/Products/Pagination/${page}/${counterPerPage}`
    );
  }

  getProducts() {
    return this.http.get(`${this.BASE_URL}Products`);
  }

  getProductsbyCategory(subCategoryId: any) {
    return this.http.get(
      `https://localhost:44341/api/Category/PrdouctsForCategory/${subCategoryId}`
    );
  }

  getParentCategories(): Observable<any> {
    return this.http.get(
      'https://localhost:44341/api/Category/ParentCategories'
    );
  }

  getSubcategories(parentCategoryId: any) {
    return this.http.get(
      `https://localhost:44341/api/Category/subCategories/${parentCategoryId}`
    );
  }

  getProductsByParentCategoryId(parentCategoryId: any) {
    return this.http.get(
      `https://localhost:44341/api/Category/PrdouctByParentCategory/${parentCategoryId}`
    );
  }

  getProductsUnique(id: any) {
    return this.http.get(`https://localhost:44341/api/Products/UniqueProducts`);
  }

  getAllCategories() {
    return this.http.get('https://localhost:44341/api/Category');
  }

  addProduct(AddProduct: any) {
    console.log(AddProduct);
    return this.http.post(
      'https://localhost:44341/api/Products/Add',
      AddProduct
    );
  }
  getProductById(ProductId = 'b9cf58dc-81cf-478e-a45a-11313e09ac95') {
    return this.http.get(
      `https://localhost:44341/api/Products/Update/${ProductId}`
    );
  }
  updateProductBy(UpdatedProduct: any) {
    console.log(UpdatedProduct);
    return this.http.put(
      'https://localhost:44341/api/Products/Update',
      UpdatedProduct
    );
  }
  SaveImages(Images: FileList) {
    console.log(Image);
    const formData = new FormData();
    for (let i = 0; i < Images.length; i++) {
      formData.append('Images', Images[i], Image.name);
      console.log(Images[i] as File);
    }
    return this.http.post(
      'https://localhost:44341/api/Products/Images',
      formData
    );
  }

  DeleteProduct(productId: any) {
    return this.http.delete(`${this.BASE_URL}Products/Delete/${productId}`);
  }
}
