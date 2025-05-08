import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Brande Admin Dashboard';

  constructor(private router: Router) {}

  /**
   * Handle admin logout
   */
  LogOut() {
    // Clear any stored tokens or session data
    localStorage.removeItem('adminToken');
    localStorage.removeItem('adminData');

    // Redirect to login page or home page
    // Note: Update this to your actual login page route
    this.router.navigate(['/']);

    // Show logout confirmation
    alert('You have been logged out successfully.');
  }
}
