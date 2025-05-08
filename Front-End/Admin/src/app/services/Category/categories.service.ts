import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  constructor(private http: HttpClient) { }

  addCategory(AddCategory: any) {
    console.log(AddCategory);
    return this.http.post(
      'https://localhost:44341/api/Category/Add',
      AddCategory
    );
  }

  getParentCategories() {
    return this.http.get(
      'https://localhost:44341/api/Category/ParentCategories'
    );
  }

  getSubcategories(parentCategoryId: any) {
    return this.http.get(
      `https://localhost:44341/api/Category/subCategories/${parentCategoryId}`
    );
  }

  getAllCategories() {
    return this.http.get('https://localhost:44341/api/Category');
  }
}
