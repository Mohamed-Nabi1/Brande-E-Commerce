import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Route, Router } from '@angular/router';
import { CategoriesService } from 'src/app/services/Category/categories.service';
import { ProductsService } from 'src/app/services/Products/products.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent implements OnInit {
  AllCategories: any[] | any = [];
  AllSubCategories: any[] | any = [];
  ParentCategories: any[] = [];
  SubCategories: any[] = [];
  selectedParentId: any;
  selectedSubItem: any;
  subItemsDisabled = true;
  productInfo: { color: any; size: any; quantity: any }[] = [];
  productImages: any[] = [];
  productCategories: any[] = [];

  constructor(
    private ProductService: ProductsService,
    private build: FormBuilder,
    private CategoryService: CategoriesService,
    private router: Router
  ) {
    // this.getAllCatergories();
    this.validFrm.get('PSubCat')?.disable();
  }

  ngOnInit(): void {
    this.getAllCatergories();
  }

  //getallcategories
  getAllCatergories() {
    this.ProductService.getAllCategories().subscribe((res) => {
      this.AllCategories = res;
      console.log(this.AllCategories);
      for (var i = 0; i < this.AllCategories.length; i++) {
        // console.log('loop');

        if (this.AllCategories[i].parentCategoryId != null) {
          this.AllSubCategories.push(this.AllCategories[i]);
        } else {
          this.ParentCategories.push(this.AllCategories[i]);
        }
      }
      console.log(this.AllSubCategories);
      console.log(this.ParentCategories);
    });
  }

  duplicateError: boolean = false;

  // deleteProductInfo(index: number) {
  //   this.productInfo.splice(index, 1);

  //   console.log(this.productInfo);
  // }
  isSuccess: boolean = false;

  addCategory(name: any, description: any, parentCategory: any) {
    let AddCategory = {
      Name: name,
      Description: description,
      Image: 'string',
      ParentCategoryId: parentCategory,
    };
    if (this.validFrm.valid) console.log(AddCategory);
    this.CategoryService.addCategory(AddCategory).subscribe({
      next: () => {
        console.log('Added Success');
        console.log(AddCategory);
        this.productInfo = [];
        this.isSuccess = true;
      },
      error: (err) => {
        err;
        console.log(err)
        console.log('Fail');
      },
    });
  }

  //Select Items
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

  //validation

  validFrm = new FormGroup({
    PName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    PDescription: new FormControl('', [
      Validators.required,
      Validators.maxLength(500),
    ]),

    // PIMGS: new FormControl('', Validators.required),
    PInfo: new FormControl('', [
      Validators.required,
      Validators.maxLength(500),
    ]),
    PCat: new FormControl('', [Validators.required]),
    PIMGS: new FormControl('', [Validators.required]),
  });

  keyPressNumbers(event: any, len: number) {
    let inp = String.fromCharCode(event.keyCode);
    /[0-9]/.test(inp) ? true : event.preventDefault();

    let inp1 = event.target.value;
    inp1.length < len ? true : event.preventDefault();
  }

  keyPressMinLength(event: any, len: number) {
    let inp = event.target.value;
    inp.length < len ? true : event.preventDefault();
  }

  goCategoryList() {
    this.router.navigateByUrl('categories');
  }
}
