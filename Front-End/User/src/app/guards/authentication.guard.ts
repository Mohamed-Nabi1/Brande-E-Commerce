import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CustomerService } from '../services/Customer/customer.service';

export const authenticationGuard: CanActivateFn = (route, state) => {
  const authenticationService = inject(CustomerService);
  const router = inject(Router);

  if (authenticationService.isLoggedIn$.value) {
    return true;
  }

  router.navigateByUrl('/login');
  return false;
};
