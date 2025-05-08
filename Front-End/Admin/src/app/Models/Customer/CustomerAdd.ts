export interface CustomerAdd {
  FirstName:any;
  MidName: any;
  LastName: any;
  PhoneNumber:any;

  Street: any;
  City: any;
  Country: any;

  Email: any;
  Password: any;
}


export interface Customer {
  id: string;
  email: string;
  phoneNumber: string;
  firstName: string;
  midName: string;
  lastName: string;
  street: string;
  city: string;
  country: number;
}