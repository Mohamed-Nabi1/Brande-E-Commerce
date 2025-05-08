import { HttpErrorResponse } from '@angular/common/http';
import { Component} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Countries } from 'src/app/Models/CountriesEnums/Country';
import { CustomerAdd } from 'src/app/Models/Customer/CustomerAdd';
import { CustomerService } from 'src/app/services/Customer/customer.service';
import { __values } from 'tslib';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {

  mname="";
  selectedCountry = Countries.Egypt;
  showPassword = false;

  message: any;
  displayAlert = 'd-none';
  FlagError = false;

  Customer?: CustomerAdd;
  constructor(protected _ser: CustomerService, protected router: Router) {}

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  validFrm = new FormGroup({
    fname: new FormControl('', [Validators.required, Validators.minLength(3)]),
    lname: new FormControl('', [Validators.required, Validators.minLength(3)]),

    phonenumber: new FormControl('', [
      Validators.required,
      Validators.minLength(9),
    ]),
    country: new FormControl('', Validators.required),
    city: new FormControl('', [Validators.required, Validators.minLength(3)]),
    street: new FormControl('', [Validators.required, Validators.minLength(3)]),

    email: new FormControl('', [
      Validators.required,
      Validators.email,
      Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
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

  AddNewCustomer() {
    if (this.validFrm.invalid) {
      
      // Display error messages
      Object.keys(this.validFrm.controls).forEach((field) => {
        if (field != 'mname') {
          const control = this.validFrm.get(field);
          if (control?.invalid) {
            control.markAsTouched({ onlySelf: true });
            //console.log(control);
          }
        }
      });
      //console.log('I am Invalid');
    } else if (this.validFrm.valid) {
      this._ser
        .AddCustomer({
          FirstName: this.validFrm.value.fname,
          MidName: this.mname,
          LastName: this.validFrm.value.lname,
          PhoneNumber: this.validFrm.value.phonenumber,

          Street: this.validFrm.value.street,
          City: this.validFrm.value.city,
          Country: this.validFrm.value.country,

          Email: this.validFrm.value.email,
          Password: this.validFrm.value.password,
        })
        .subscribe({
          next: () => {
            this.router.navigateByUrl('/login');
          },
          error: (error: HttpErrorResponse) => {
            let errorMessage = 'An error occurred';
            if (error.error instanceof Array) {
              console.log(errorMessage);
              errorMessage = error.error
                .map((err: any) => err.description)
                .join(',');
            } else if (typeof error.error === 'object') {
              errorMessage = error.error.description;
            } else if (typeof error.error === 'string') {
              errorMessage = error.error;
            }
            let part = errorMessage.split(',');
            this.openDialog(part[1], true);
          },
        });
    }
  }

  openDialog(mess: string, errFlag: boolean) {
    this.message = mess;
    this.FlagError = errFlag;
    this.displayAlert = 'd-block';
  }

  closePopup() {
    this.displayAlert = 'd-none';
    if (this.FlagError) {
      this.validFrm.get('email')?.setValue('');
    } else {
      Object.keys(this.validFrm.controls).forEach((key) => {
        this.validFrm.get(key)?.setValue('');
      });
    }
  }

  Countries = [
    'Afghanistan',
    'Albania',
    'Algeria',
    'Andorra',
    'Angola',
    'Anguilla',
    'Antarctica',
    'Argentina',
    'Armenia',
    'Aruba',
    'Australia',
    'Austria',
    'Azerbaijan',
    'Bahamas',
    'Bahrain',
    'Bangladesh',
    'Barbados',
    'Belarus',
    'Belgium',
    'Belize',
    'Benin',
    'Bermuda',
    'Bhutan',
    'Bolivia',
    'Bonaire',
    'Botswana',
    'Brazil',
    'British',
    'Brunei',
    'Bulgaria',
    'Burundi',
    'Cambodia',
    'Cameroon',
    'Canada',
    'Chad',
    'Chile',
    'China',
    'Colombia',
    'Comoros',
    'Congo',
    'Croatia',
    'Cuba',
    'Curaçao',
    'Cyprus',
    'Czechia',
    'Denmark',
    'Djibouti',
    'Dominica',
    'Ecuador',
    'Egypt',
    'Eritrea',
    'Estonia',
    'Ethiopia',
    'Fiji',
    'Finland',
    'France',
    'Gabon',
    'Gambia',
    'Georgia',
    'Germany',
    'Ghana',
    'Gibraltar',
    'Greece',
    'Greenland',
    'Grenada',
    'Guadeloupe',
    'Guam',
    'Guatemala',
    'Guernsey',
    'Guinea',
    'Guyana',
    'Haiti',
    'Hungary',
    'Iceland',
    'India',
    'Indonesia',
    'Iraq',
    'Ireland',
    'Italy',
    'Jamaica',
    'Japan',
    'Jersey',
    'Jordan',
    'Kazakhstan',
    'Kenya',
    'Kiribati',
    'Kuwait',
    'Kyrgyzstan',
    'Latvia',
    'Lebanon',
    'Lesotho',
    'Liberia',
    'Libya',
    'Liechtenstein',
    'Lithuania',
    'Luxembourg',
    'Macao',
    'Madagascar',
    'Malawi',
    'Malaysia',
    'Maldives',
    'Mali',
    'Malta',
    'Martinique',
    'Mauritania',
    'Mauritius',
    'Mayotte',
    'Mexico',
    'Monaco',
    'Mongolia',
    'Montenegro',
    'Montserrat',
    'Morocco',
    'Mozambique',
    'Myanmar',
    'Namibia',
    'Nauru',
    'Nepal',
    'Netherlands',
    'Nicaragua',
    'Niger',
    'Nigeria',
    'Norway',
    'Oman',
    'Pakistan',
    'Palau',
    'Panama',
    'Paraguay',
    'Peru',
    'Philippines',
    'Pitcairn',
    'Poland',
    'Portugal',
    'Qatar',
    'Réunion',
    'Romania',
    'Rwanda',
    'Samoa',
    'Senegal',
    'Serbia',
    'Seychelles',
    'Singapore',
    'Slovakia',
    'Slovenia',
    'Somalia',
    'Spain',
    'Sudan',
    'Suriname',
    'Swaziland',
    'Sweden',
    'Switzerland',
    'Taiwan',
    'Tajikistan',
    'Tanzania',
    'Thailand',
    'Togo',
    'Tokelau',
    'Tonga',
    'Tunisia',
    'Turkey',
    'Turkmenistan',
    'Tuvalu',
    'Uganda',
    'Ukraine',
    'Uruguay',
    'Uzbekistan',
    'Vanuatu',
    'Yemen',
    'Zambia',
    'Zimbabwe',
  ];
}
