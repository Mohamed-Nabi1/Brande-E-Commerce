import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/Customer/customer.service';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css'],
})
export class AboutUsComponent implements OnInit {
  dataArray: string[] = [];
  constructor(private _ser: CustomerService) {}

  ngOnInit(): void {
    this._ser.getTestData().subscribe((value) => (this.dataArray = value));
  }
}
