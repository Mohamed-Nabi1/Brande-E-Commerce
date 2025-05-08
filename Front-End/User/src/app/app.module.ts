import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { FooterComponent } from './components/footer/footer.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CartComponent } from './components/cart/cart.component';
import { SubCategoryComponent } from './components/sub-category/sub-category.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { WishListComponent } from './components/wish-list/wish-list.component';

import { AboutUsComponent } from './components/about-us/about-us.component';
import { ErrorComponent } from './components/error/error.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import {
  MatStepperModule,
  matStepperAnimations,
} from '@angular/material/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { RatingModule } from 'ngx-bootstrap/rating';
import { AdminPanelComponent } from './components/Admin - Components/admin-panel/admin-panel.component';
import { CheckOutComponent } from './components/check-out/check-out.component';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { ProfileInfoComponent } from './components/ProfileInfo/profile-info.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LoadingInterceptor } from './Interceptor/loading.interceptor';
import { OrderDetailsComponent } from './order-details/order-details.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    CartComponent,
    SubCategoryComponent,
    LoginComponent,
    RegisterComponent,
    ProductsComponent,
    ProductDetailsComponent,
    ResetPasswordComponent,
    WishListComponent,
    AboutUsComponent,
    ErrorComponent,
    AdminPanelComponent,
    CheckOutComponent,
    ProfileComponent,
    ProfileInfoComponent,
    OrderDetailsComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatStepperModule,
    FormsModule,
    MatTableModule,
    NgxPaginationModule,
    PaginationModule.forRoot(),
    RatingModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
