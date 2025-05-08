import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-sub-category',
  templateUrl: './sub-category.component.html',
  styleUrls: ['./sub-category.component.css'],
})
export class SubCategoryComponent implements OnInit {
  constructor(
    private subCatServices: CategoryService,
    private urlData: ActivatedRoute
  ) {}
  subCat: any;
  categoryId = this.urlData.snapshot.params['id'];

  ngOnInit(): void {
    this.subCatServices.GetSubCategories(this.categoryId).subscribe({
      next: (data: any) => {
        console.log(this.urlData);
        this.subCat = data;
      },
      error: () => {},
    });
  }
}
