import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private readonly myClient: HttpClient) { }
  private readonly API_URL = 'https://localhost:44341/api/';
  GetAllCategries() {
    return this.myClient.get(this.API_URL + 'Category');
  }

  GetParentCategories() {
    return this.myClient.get(this.API_URL + 'Category/ParentCategories');
  }

  GetSubCategories(id: any) {
    return this.myClient.get(this.API_URL + 'Category/subCategories/' + id);
  }

  GetAllSubCategoriesUnique() {
    return this.myClient.get(this.API_URL + 'Category/CategoryUnique');
  }
}
