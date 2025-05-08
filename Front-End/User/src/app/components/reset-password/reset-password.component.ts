import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from 'src/app/services/Customer/customer.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  message: string = '';
  displayAlert: string = 'd-none';
  FlagError: boolean = false;
  passwordStrength: string = '';
  passwordsMatch: boolean = false;

  // Toggle password visibility
  p: boolean = false;
  np: boolean = false;
  cp: boolean = false;

  constructor(protected _ser: CustomerService, private router: Router) {}

  ngOnInit(): void {
    // Initialize form and add listeners for password strength
    this.passFrm.get('npassword')?.valueChanges.subscribe(value => {
      this.checkPasswordStrength(value || '');
      // Revalidate password match when new password changes
      this.validatePassword();
    });

    // Add listener for confirm password field
    this.passFrm.get('cpassword')?.valueChanges.subscribe(() => {
      this.validatePassword();
    });
  }

  passFrm = new FormGroup({
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    npassword: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    cpassword: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
  });

  /**
   * Prevents input beyond the specified length
   */
  keyPressMinLength(event: any, len: number) {
    let inp = event.target.value;
    inp.length < len ? true : event.preventDefault();
  }

  /**
   * Validates that confirm password matches new password
   */
  validatePassword() {
    const pass = this.passFrm.get('npassword')?.value || '';
    const confirmPassword = this.passFrm.get('cpassword')?.value || '';

    if (pass !== confirmPassword) {
      this.passFrm.get('cpassword')?.setErrors({ passwordMismatch: true });
      this.passwordsMatch = false;
    } else {
      this.passFrm.get('cpassword')?.setErrors(null);
      this.passwordsMatch = true;
    }
  }

  /**
   * Checks password strength based on length and complexity
   */
  checkPasswordStrength(password: string) {
    if (!password) {
      this.passwordStrength = '';
      return;
    }

    // Basic strength check based on length
    if (password.length < 8) {
      this.passwordStrength = 'weak';
    } else if (password.length < 12) {
      this.passwordStrength = 'medium';
    } else {
      this.passwordStrength = 'strong';
    }
  }

  /**
   * Toggles password visibility for the specified field
   */
  togglePassword(id: string) {
    switch (id) {
      case 'p':
        this.p = !this.p;
        break;
      case 'np':
        this.np = !this.np;
        break;
      case 'cp':
        this.cp = !this.cp;
        break;
      default:
        break;
    }
  }

  /**
   * Handles password change submission
   */
  ChangePassword() {
    if (this.passFrm.invalid) {
      // Display error messages
      Object.keys(this.passFrm.controls).forEach((field) => {
        const control = this.passFrm.get(field);
        if (control?.invalid) {
          control.markAsTouched({ onlySelf: true });
        }
      });
    } else if (this.passFrm.valid) {
      this._ser
        .ResetPassword({
          currentPassword: this.passFrm.value.password,
          newPassword: this.passFrm.value.npassword,
        })
        .subscribe({
          next: (response) => {
            this.openDialog('Password updated successfully!', false);
            setTimeout(() => {
              this.closePopup();
              this.router.navigateByUrl('/profileInfo');
            }, 2000);
          },
          error: (error: HttpErrorResponse) => {
            if (error.status == 201) {
              this.openDialog('Password updated successfully!', false);
              setTimeout(() => {
                this.closePopup();
                this.router.navigateByUrl('/profileInfo');
              }, 2000);
            } else {
              this.openDialog('Current password is incorrect. Please try again.', true);
            }
          }
        });
    }
  }

  /**
   * Opens the dialog modal with a message
   */
  openDialog(mess: string, errFlag: boolean) {
    this.message = mess;
    this.FlagError = errFlag;
    this.displayAlert = 'd-block';
  }

  /**
   * Closes the dialog modal and resets form if needed
   */
  closePopup() {
    this.displayAlert = 'd-none';
    if (this.FlagError) {
      this.passFrm.get('password')?.setValue('');
    } else {
      Object.keys(this.passFrm.controls).forEach((key) => {
        this.passFrm.get(key)?.setValue('');
      });
    }
  }
}
