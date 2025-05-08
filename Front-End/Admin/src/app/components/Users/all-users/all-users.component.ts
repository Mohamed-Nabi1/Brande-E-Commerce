import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/Customers/customer.service';
import { getCountryNameById, getCountriesList } from 'src/app/Models/CountriesEnums/Country';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent implements OnInit {
  searchInput: string = '';
  customers: any[] = [];
  filteredCustomers: any[] = [];
  selectedCustomer: any = null;
  countries = getCountriesList();
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.isLoading = true;
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        this.filteredCustomers = [...this.customers];
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading customers:', err);
        this.errorMessage = 'Failed to load customers. Please try again.';
        this.isLoading = false;
      }
    });
  }

  filterCustomers(): void {
    if (!this.searchInput) {
      this.filteredCustomers = [...this.customers];
      return;
    }
    
    const searchTerm = this.searchInput.toLowerCase();
    this.filteredCustomers = this.customers.filter(customer =>
      customer.firstName.toLowerCase().includes(searchTerm) ||
      customer.lastName.toLowerCase().includes(searchTerm) ||
      customer.email.toLowerCase().includes(searchTerm) ||
      customer.phoneNumber.includes(searchTerm) ||
      this.getCountryName(customer.country).toLowerCase().includes(searchTerm)
    );
  }

  viewCustomerDetails(customer: any): void {
    this.selectedCustomer = customer;
  }

  closeDetails(): void {
    this.selectedCustomer = null;
  }

  getCountryName(countryId: number): string {
    return getCountryNameById(countryId);
  }
}