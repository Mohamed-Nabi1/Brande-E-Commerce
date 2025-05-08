import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductsService } from 'src/app/services/Products/products.service';

@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.css', './all-products.component.scss']
})
export class AllProductsComponent implements OnInit {
  productFromDb: any;
  constructor(private readonly productService: ProductsService,
    private build: FormBuilder) {

    this.validFrm.get('PSubCat')?.disable();
    this.UvalidFrm.get('uSubCat')?.disable();
  }

  allProducts: any;
  searchInput: string = '';
  AllCategories: any[] | any = [];
  AllSubCategories: any[] | any = [];
  ParentCategories: any[] = [];
  SubCategories: any[] = [];
  selectedParentId: any;
  selectedSubItem: any;
  
  ProductToUpdate:any;
  ProductToDelete:any;

  subItemsDisabled = true;
  quantity: any;
  color: any;
  size: any;
  Uquantity: any;
  Ucolor: any;
  Usize: any;
  productInfo: { color: any; size: any; quantity: any }[] = [];
  productImages!: FileList;
  isSuccess: boolean = false;
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
  duplicateError: boolean = false;

  public imageUrls: string[] = [];

  //colors: [] | any = [];
  //sizes: [] | any = [];
  //name: any;
  //productCategories: any[] = [];
  
  
  ngOnInit(): void {
    this.productService.getProducts().subscribe({
      next: (data) => {
        this.allProducts = data;
        this.getAllCatergories();

      },
      error: (error) => console.log(error)
    })

  }
  get filteredProducts() {
    return this.allProducts.filter((product: { name: string; }) => 
    product.name.toLowerCase().includes(this.searchInput.toLowerCase()));
  }
  getAllCatergories() {
    this.productService.getAllCategories().subscribe((res) => {
      this.AllCategories = res;
      //console.log(this.AllCategories);
      for (var i = 0; i < this.AllCategories.length; i++) {
        // console.log('loop');

        if (this.AllCategories[i].parentCategoryId != null) {
          this.AllSubCategories.push(this.AllCategories[i]);
        } else {
          this.ParentCategories.push(this.AllCategories[i]);
        }
      }
      //console.log(this.AllSubCategories);
      //console.log(this.ParentCategories);
    });
  }
  //Add product info


  AddProductInfo() {
    // if (this.productInfo.length >= 3) {
    //   console.log('Maximum number of rows 3');
    //   return;
    // }
    if (!this.color || !this.size || !this.quantity) {
      //console.log(' Fill all fields.');
      return;
    }
    const isDuplicate = this.productInfo.some(
      (info) => info.color === this.color && info.size === this.size
    );

    if (isDuplicate) {
      //console.log('Duplicate Data');
      //console.log(isDuplicate);
      this.duplicateError = true;
      return;
    }

    let Infos: any = {
      color: this.color,
      size: this.size,
      quantity: this.quantity,
    };
    //console.log(Infos);
    this.productInfo.push(Infos);
    //console.log(this.productInfo);
    this.isSuccess = true;
    this.duplicateError = false;
  }

  deleteProductInfo(index: number) {
    this.productInfo.splice(index, 1);

    //console.log(this.productInfo);
  }
  clearProductInfo(){
    this.productInfo = [];
  }
  //add product
  addProduct(name: any, description: any, price: any, discount: any, id: any, catId: any) {
    console.log(this.validFrm.value)
  if (!this.validFrm.valid ) {
    console.log("yyy");
    this.validFrm.markAllAsTouched();
  }
  else {
    console.log("xx");
    console.log(this.productImages)
    
      this.productService.SaveImages(this.productImages).subscribe({
        next:(data)=>{console.log(data);
          let AddProduct = {
          name: name,
          description: description,
          price: price,
          discount: discount,
          Images: data,
          productCategories: [ id ,  catId ],
          productInfo: this.productInfo,
        };
        console.log("add product")
        console.log(AddProduct);
        this.productService.addProduct(AddProduct).subscribe({
        next: () => {
          console.log('Added Success');
          console.log(AddProduct);
          this.productInfo = [];
          this.isSuccess = true;
          this.ngOnInit();
        },
        error: (err) => {
          err;
          console.log(err);
        },
      });
      },
        error: (error)=>{console.log("error")}
      })

      
    }

  }

  //Select Items
  onItemSelected() {
    //console.log(this.selectedParentId);

    if (this.selectedParentId != null) {
      this.SubCategories = this.AllSubCategories.filter(
        (subItem: { parentCategoryId: String }) =>
          subItem.parentCategoryId === this.selectedParentId
      );
      //console.log(this.SubCategories);
      this.validFrm.get('PSubCat')?.enable();
      this.UvalidFrm.get('uSubCat')?.enable();
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
    Images: new FormControl('', Validators.required),
    Pcolor: new FormControl('', Validators.required),
    // PInfo: new FormControl('', [
    //   Validators.required,
    //   Validators.maxLength(500),
    // ]),
    PSize: new FormControl('', [Validators.required]),
    PQuantity: new FormControl('', [Validators.required]),
    PCat: new FormControl('', [Validators.required]),
    PSubCat: new FormControl({ disabled: true }, [Validators.required]),
  });

  UvalidFrm = new FormGroup({
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
    uIMGS: new FormControl('', Validators.required),
    ucolor: new FormControl('', Validators.required),
    
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

  
  public onFileSelected(event: Event): void {
    this.imageUrls = [];
    const inputElement = event.target as HTMLInputElement;
    if (inputElement && inputElement.files && inputElement.files.length > 0) {
      this.productImages = inputElement.files;

      for (let i = 0; i < inputElement.files.length; i++) {
        const file:File = inputElement.files[i];
        if (file && file.type.startsWith('image/')) {
          const reader = new FileReader();
          reader.onload = () => {
            this.imageUrls.push(reader.result as string);
          };
          reader.readAsDataURL(file);

        }
      }
      console.log(this.productImages)
    }
  }

  SelectedUpdate(product: any) {
    this.ProductToUpdate = product;
    console.log('select update');
    this.productService.getProductById(product.id).subscribe({
      next: (data: any) => {
        this.productFromDb = data;
        this.productInfo = data.productInfo;
        console.log(this.productInfo);
        console.log(data);
      },
      error: (err) => {
        err;
      },
    });

   // this.fillInputs();
  }

  SelectedDelete(product: any) {
    console.log('delete');
    console.log(product);
    this.ProductToDelete = product;
  }

  DeleteSelectedProduct(){
    this.productService.DeleteProduct(this.ProductToDelete.id).subscribe({
      next:(data)=>{
                    console.log('Deleted');
                    console.log(data);
                    this.ngOnInit();
                  },
      error:(error)=>{
                      console.log('Error Delete');
                      console.log(error)
                    }
    })
  }


  UpdateProduct(name: any, description: any, price: any, discount: any, subCatid: any,catId:any) {
    // let UpdateProduct = {
    //   id: this.ProductToUpdate.id,
    //   name: name,
    //   description: description,
    //   price: price,
    //   discount: discount,
    //   productImages: [{}],
    //   productCategories: [ subCatid,catId ],
    //   productInfo: this.productInfo,
    // };
    // if (this.validFrm.valid) console.log(AddProduct);
    // this.productService.updateProductBy(AddProduct).subscribe({
    //   next: () => {
    //     console.log('Updated Success');
    //     console.log(AddProduct);
    //     this.isSuccess = true;
    //   },
    //   error: (err) => {
    //     err;
    //     console.log('Fail');
    //   },
    // });
    console.log({name:name,description:description,price:price,discount:discount,subCatid:subCatid,catId:catId})
    console.log(this.UvalidFrm.value);
    if (this.UvalidFrm.valid) {
      
      console.log("------------------------");
      console.log(this.productImages)
      
        this.productService.SaveImages(this.productImages).subscribe({
          next:(data)=>{console.log(data);
            let UpdateProduct = {
            id:this.ProductToUpdate.id,
            name: name,
            description: description,
            price: price,
            discount: discount,
            Images: data,
            productCategories: [ subCatid ,  catId ],
            productInfo: this.productInfo,
          };
          console.log("Update product")
          console.log(UpdateProduct);
          this.productService.updateProductBy(UpdateProduct).subscribe({
          next: () => {
            console.log('Updated Success');
            console.log(UpdateProduct);
            this.productInfo = [];
            this.isSuccess = true;
            this.ngOnInit();
          },
          error: (err) => {
            err;
            console.log(err);
          },
        });
        },
          error: (error)=>{console.log("Update error")}
        })
    }

    else {
      console.log("Not Valid");
      this.UvalidFrm.markAllAsTouched();
      }
  
  }




}


