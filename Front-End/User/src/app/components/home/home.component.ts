import { Component, OnInit, AfterViewInit, HostListener } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  categories: any;
  categories2: any;
  fillCategories: any;

  constructor(private myService: CategoryService) {}

  ngOnInit(): void {
    this.myService.GetParentCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => { console.log(err) }
    });

    this.myService.GetAllSubCategoriesUnique().subscribe({
      next: (data) => {
        this.categories2 = data;

        this.fillCategories = this.categories2.filter((c: { name: string; }) => {
          if (c.name === 'Men' || c.name === 'Women' || c.name === 'Kids') {
            return false;
          }
          return true;
        }).slice(0, 3);
      },
      error: (err) => { console.log(err) }
    });
  }

  ngAfterViewInit(): void {
    // Initial check for elements in viewport
    this.checkScrollAnimation();
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    // Check for animations when scrolling
    this.checkScrollAnimation();
  }

  /**
   * Checks if elements with the animate-on-scroll class are in the viewport
   * and adds the appropriate animation classes
   */
  checkScrollAnimation(): void {
    const animatedElements = document.querySelectorAll('.animate-on-scroll');

    animatedElements.forEach((element: Element) => {
      if (this.isElementInViewport(element)) {
        // Keep existing classes and remove only the animate-on-scroll class
        const classList = element.classList;
        classList.remove('animate-on-scroll');
      }
    });
  }

  /**
   * Determines if an element is in the viewport
   * @param element The element to check
   * @returns boolean True if the element is in the viewport
   */
  isElementInViewport(element: Element): boolean {
    const rect = element.getBoundingClientRect();
    const windowHeight = window.innerHeight || document.documentElement.clientHeight;

    // Consider element in viewport if it's top is in view or its bottom is in view
    // and it's not too far above the top of the viewport
    return (
      (rect.top <= windowHeight * 0.8 && rect.bottom >= 0) ||
      (rect.bottom >= 0 && rect.top <= windowHeight * 0.8)
    );
  }
}
