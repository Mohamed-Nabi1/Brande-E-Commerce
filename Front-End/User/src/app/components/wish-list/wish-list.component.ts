import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WishListService } from 'src/app/services/wish-list.service';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent {

  emptylst=false;
  whishListItems: any[] = [];
  displayedColumns: string[] = [
    'image',
    'name',
    'price',
    'dicount',
    'actions',
  ];
  constructor(private myService: WishListService, public route: Router) {}

  ngOnInit() {
    this.myService.getWishListByCustomerId()
      .subscribe(
        (response) => {
          console.log(response);
          if (Object.keys(response.products).length === 0) {
            this.emptylst=true
          } else {
          this.whishListItems = response.products.map((item: any) => ({
            image: item.imageUrl || '',
            name: item.productName,
            price: item.price,
            discount: item.discount,
            pid:item.productId
          }));
          //console.log(this.whishListItems);
        }
        },
        (error) => {
          console.error('Failed to get WishList products', error);
        }
      );
  }


  deleteItem: any;
  deleteWishListItem(item: any) {
    this.myService.deleteProductInWishlist(item).subscribe(
      () => {
        console.log('Item deleted');
        this.ngOnInit();
      },
      (error: any) => {
        console.error('Failed to delete item', error);
      }
    );
  }

}


