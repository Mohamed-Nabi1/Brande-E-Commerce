import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProductsService } from 'src/app/services/Products/products.service';
@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css'],
})
export class UpdateProductComponent implements OnInit {
  ID: any;
 // AllCategories: any[] | any = [];
  //uAllSubCategories: any[] | any = [];
  uParentCategories: any[] = [];
  uSubCategories: any[] = [];
  uselectedParentId: any;
  uselectedSubItem: any;
  uproductFromDb: any[] | any = [];
  colors: [] | any = [];
  usizes: [] | any = [];
  usubItemsDisabled = true;
  uquantity: any;
  ucolor: any;
  usize: any;
  uname: any;
  uproductInfo: { color: any; size: any; quantity: any }[] = [];
  uproductImages: any[] = [];
  uproductCategories: any[] = [];

  constructor(
    private ProductService: ProductsService,
    private build: FormBuilder,
   // private route:ActivatedRoute
  ) {
    // this.getAllCatergories();
    //this.ID = route.snapshot.params["id"];
    this.validFrm.get('PSubCat')?.disable();
  }

  ngOnInit(): void {
    //this.ugetAllCatergories();

    this.ProductService.getProductById(this.ID).subscribe({
      next: (data: any) => {
        this.uproductFromDb = data;
        this.uproductInfo = data.productInfo;
        console.log(this.uproductInfo);
        console.log(data);
      },
      error: (err) => {
        err;
      },
    });
  }

  //getallcategories
  // ugetAllCatergories() {
  //   this.ProductService.getAllCategories().subscribe((res) => {
  //     this.AllCategories = res;
  //     console.log(this.AllCategories);
  //     for (var i = 0; i < this.AllCategories.length; i++) {
  //       // console.log('loop');

  //       if (this.AllCategories[i].parentCategoryId != null) {
  //         this.uAllSubCategories.push(this.AllCategories[i]);
  //       } else {
  //         this.uParentCategories.push(this.AllCategories[i]);
  //       }
  //     }
  //     console.log(this.uAllSubCategories);
  //     console.log(this.uParentCategories);
  //   });
  // }
  //Add product info

  duplicateError: boolean = false;

  uAddProductInfo() {
    // if (this.productInfo.length >= 3) {
    //   console.log('Maximum number of rows 3');
    //   return;
    // }
    if (!this.ucolor || !this.usize || !this.uquantity) {
      console.log(' Fill all fields.');
      return;
    }
    const isDuplicate = this.uproductInfo.some(
      (info) => info.color === this.ucolor && info.size === this.usize
    );

    if (isDuplicate) {
      console.log('Duplicate Data');
      console.log(isDuplicate);
      this.duplicateError = true;
      return;
    }

    let Infos: any = {
      color: this.ucolor,
      size: this.usize,
      quantity: this.uquantity,
    };
    console.log(Infos);
    this.uproductInfo.push(Infos);
    console.log(this.uproductInfo);
    this.isSuccess = true;
    this.duplicateError = false;
  }

  udeleteProductInfo(index: number) {
    this.uproductInfo.splice(index, 1);

    console.log(this.uproductInfo);
  }
  //add product
  isSuccess: boolean = false;

  uaddProduct(name: any, description: any, price: any, discount: any, id: any) {
    let AddProduct = {
      id: '52013B52-A1E4-44F1-85E7-426482C144B5',
      name: name,
      description: description,
      price: price,
      discount: discount,
      productImages: [{}],
      productCategories: [{ id }],
      productInfo: this.uproductInfo,
    };
    if (this.validFrm.valid) console.log(AddProduct);
    this.ProductService.updateProductBy(AddProduct).subscribe({
      next: () => {
        console.log('Updated Success');
        console.log(AddProduct);
        this.isSuccess = true;
      },
      error: (err) => {
        err;
        console.log('Fail');
      },
    });
  }

  //Select Items
   uonItemSelected() {
  //   console.log(this.uselectedParentId);

  //   if (this.uselectedParentId != null) {
  //     this.uSubCategories = this.uAllSubCategories.filter(
  //       (subItem: { parentCategoryId: String }) =>
  //         subItem.parentCategoryId === this.uselectedParentId
  //     );
  //     console.log(this.uSubCategories);
  //     this.validFrm.get('PSubCat')?.enable();
  //   }
   }

  //validation

  validFrm = new FormGroup({
    uName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    uDescription: new FormControl('', [
      Validators.required,
      Validators.maxLength(500),
    ]),
    uPrice: new FormControl('', [Validators.required, Validators.min(0)]),

    uDiscount: new FormControl('', [
      Validators.required,
      //Validators.pattern(/^\d{11}$/),
      Validators.min(0),
      Validators.max(1),
    ]),
    // PIMGS: new FormControl('', Validators.required),
    Pcolor: new FormControl('', Validators.required),
    uInfo: new FormControl('', [
      Validators.required,
      Validators.maxLength(500),
    ]),
    uSize: new FormControl('', [Validators.required]),
    uQuantity: new FormControl('', [Validators.required]),
    uCat: new FormControl('', [Validators.required]),
    uSubCat: new FormControl({ disabled: true }, [Validators.required]),
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
