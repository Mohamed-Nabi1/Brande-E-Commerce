import { AfterContentInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoriesService } from 'src/app/services/Category/categories.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],
})
export class CategoryListComponent implements OnInit {
  constructor(
    private CategoryService: CategoriesService
  ) {}
  ngOnInit(): void {
    this.getAllCatergories();
    this.validFrm.get('PSubCat')?.disable();
  }
  AllCategories: any[] | any = [];
  AllSubCategories: any[] | any = [];
  ParentCategories: any[] = [];
  SubCategories: any[] = [];
  selectedParentId: any;
  selectedSubItem: any;
  subItemsDisabled = true;
  isSuccess: boolean = false;
  


  getAllCatergories() {
    this.CategoryService.getAllCategories().subscribe((res) => {
      this.AllCategories = res;
      this.AllSubCategories = [];
      this.ParentCategories = [];
      for (var i = 0; i < this.AllCategories.length; i++) {
       
        if (this.AllCategories[i].parentCategoryId != null) {
          this.AllSubCategories.push(this.AllCategories[i]);
        } else {
          this.ParentCategories.push(this.AllCategories[i]);
        }
      }
    });
  }
  validFrm = new FormGroup({
    PName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    PDescription: new FormControl('', [
      Validators.required,
      Validators.maxLength(500),
    ]),

    PCat: new FormControl('', [Validators.required]),
  });

  addCategory(name: any, description: any, parentCategory: any) {
    let AddCategory = {
      Name: name,
      Description: description,
      ParentCategoryId: parentCategory,
    };
    if (this.validFrm.valid)
    {

      this.CategoryService.addCategory(AddCategory).subscribe({
      next: () => {
        console.log('Added Success');
        console.log(AddCategory);
        this.isSuccess = true;
        this.ngOnInit();
      },
      error: (err) => {
        err;
        console.log(err);
        console.log('Fail');
      },
    });
  }
  else{
    this.validFrm.markAllAsTouched();
  }
  }
  onItemSelected() {
    console.log(this.selectedParentId);

    if (this.selectedParentId != null) {
      this.SubCategories = this.AllSubCategories.filter(
        (subItem: { parentCategoryId: String }) =>
          subItem.parentCategoryId === this.selectedParentId
      );
      console.log(this.SubCategories);
      this.validFrm.get('PSubCat')?.enable();
    }
  }

}
