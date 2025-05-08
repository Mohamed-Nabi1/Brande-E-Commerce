import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from 'src/app/services/Customer/customer.service';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.css'],
})
export class ProfileInfoComponent implements OnInit {
  selectedCountry = ''; //<<====
  ID: any;
  customer: any;
  showPassword = false;

  message: any;
  displayAlert = 'd-none';
  FlagError = false;

  Customer: any; //<<====
  constructor(
    protected CustomerService: CustomerService,
    protected myActivated: ActivatedRoute,
    protected routr: Router
  ) {
    this.ID = myActivated.snapshot.params['id'];
    console.log(myActivated.snapshot);
    console.log(this.ID);
  }
  ngOnInit(): void {
    this.CustomerService.GetCustomer().subscribe({
      next: (data) => {
        console.log(data);
        this.customer = data;
      },
      error: (err) => {
        err;
      },
    });
  }

  validFrm = new FormGroup({
    fname: new FormControl('', [Validators.required, Validators.minLength(3)]),
    mname: new FormControl('', [Validators.nullValidator]),
    lname: new FormControl('', [Validators.required, Validators.minLength(3)]),

    phonenumber: new FormControl('', [
      Validators.required,
      //Validators.pattern(/^\d{11}$/),
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

  getCountryName(country: any) {
    return this.Countries[country];
  }
  Countries: string[] = [
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
