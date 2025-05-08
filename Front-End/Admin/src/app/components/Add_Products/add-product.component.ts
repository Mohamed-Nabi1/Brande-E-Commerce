import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ProductsService } from 'src/app/services/Products/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit {
  AllCategories: any[] | any = [];
  AllSubCategories: any[] | any = [];
  ParentCategories: any[] = [];
  SubCategories: any[] = [];
  selectedParentId: any;
  selectedSubItem: any;

  colors: [] | any = [];
  sizes: [] | any = [];
  subItemsDisabled = true;
  quantity: any;
  color: any;
  size: any;
  name: any;

  productInfo: { color: any; size: any; quantity: any }[] = [];
  productImages: any[] = [];
  productCategories: any[] = [];

  constructor(
    private ProductService: ProductsService,
    private build: FormBuilder
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
  //Add product info

  duplicateError: boolean = false;

  AddProductInfo() {
    // if (this.productInfo.length >= 3) {
    //   console.log('Maximum number of rows 3');
    //   return;
    // }
    if (!this.color || !this.size || !this.quantity) {
      console.log(' Fill all fields.');
      return;
    }
    const isDuplicate = this.productInfo.some(
      (info) => info.color === this.color && info.size === this.size
    );

    if (isDuplicate) {
      console.log('Duplicate Data');
      console.log(isDuplicate);
      this.duplicateError = true;
      return;
    }

    let Infos: any = {
      color: this.color,
      size: this.size,
      quantity: this.quantity,
    };
    console.log(Infos);
    this.productInfo.push(Infos);
    console.log(this.productInfo);
    this.isSuccess = true;
    this.duplicateError = false;
  }

  deleteProductInfo(index: number) {
    this.productInfo.splice(index, 1);

    console.log(this.productInfo);
  }
  //add product
  isSuccess: boolean = false;

  addProduct(name: any, description: any, price: any, discount: any, id: any) {
    let AddProduct = {
      name: name,
      description: description,
      price: price,
      discount: discount,
      productImages: [{}],
      productCategories: [{ id }],
      productInfo: this.productInfo,
    };
    if (this.validFrm.valid) console.log(AddProduct);
    this.ProductService.addProduct(AddProduct).subscribe({
      next: () => {
        console.log('Added Success');
        console.log(AddProduct);
        this.productInfo = [];
        this.isSuccess = true;
      },
      error: (err) => {
        err;
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
    PPrice: new FormControl('', [Validators.required, Validators.min(0)]),

    PDiscount: new FormControl('', [
      Validators.required,
      //Validators.pattern(/^\d{11}$/),
      Validators.min(0),
      Validators.max(1),
    ]),
    // PIMGS: new FormControl('', Validators.required),
    Pcolor: new FormControl('', Validators.required),
    PInfo: new FormControl('', [
      Validators.required,
      Validators.maxLength(500),
    ]),
    PSize: new FormControl('', [Validators.required]),
    PQuantity: new FormControl('', [Validators.required]),
    PCat: new FormControl('', [Validators.required]),
    PSubCat: new FormControl({ disabled: true }, [Validators.required]),
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

  Sizes = ['S', 'M', 'L', 'XL', 'XXL', 'XXXL'];
  Colors = [
    'Red',
    'Blue',
    'Green',
    'Yellow',
    'Orange',
    'Purple',
    'Pink',
    'Black',
    'White',
    'Gray',
    'Brown',
  ];
}
