import { Component, OnInit, OnDestroy, AfterViewInit, Inject, PLATFORM_ID, HostListener } from '@angular/core';
import { RouterLink, RouterOutlet, RouterLinkActive, Router, NavigationEnd } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { User } from '../models/models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, RouterOutlet, RouterLinkActive, CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit, OnDestroy, AfterViewInit {
  currentUser: User | null = null;
  sidebarCollapsed = false;
  currentPageTitle = 'Dashboard Overview';
  searchQuery = '';
  isBrowser = false; // Will be set to true only in browser
  private routerSubscription: Subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
    this.currentUser = this.authService.currentUserValue;
    // Check if we're on mobile initially
    this.checkMobileView();
  }

  get isMobileView(): boolean {
    return this.isBrowser && window.innerWidth <= 768;
  }

  private pageTitles: { [key: string]: string } = {
    'overview': 'Dashboard Overview',
    'courses': 'Course Management',
    'users': 'User Management',
    'achievements': 'Achievements',
    'lessons': 'Lesson Management',
    'quizzes': 'Quiz Management'
  };

  ngOnInit() {
    // Subscribe to router events to update page title
    this.routerSubscription = this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.updatePageTitle(event.url);
      });

    // Set initial page title
    this.updatePageTitle(this.router.url);
  }

  ngAfterViewInit() {
    // Initialize Bootstrap dropdown after view is ready
    if (this.isBrowser && typeof (window as any).bootstrap !== 'undefined') {
      const dropdownElement = document.getElementById('page-header-user-dropdown');
      if (dropdownElement) {
        new (window as any).bootstrap.Dropdown(dropdownElement);
      }
    }
  }

  ngOnDestroy() {
    this.routerSubscription.unsubscribe();
  }

  private updatePageTitle(url: string) {
    // Extract the last segment of the URL (e.g., 'overview', 'courses', etc.)
    const segments = url.split('/');
    const lastSegment = segments[segments.length - 1];

    // If it's empty (dashboard root), default to overview
    const routeKey = lastSegment || 'overview';
    this.currentPageTitle = this.pageTitles[routeKey] || 'Admin Dashboard';
  }

  onSearchInput(event: Event) {
    const target = event.target as HTMLInputElement;
    this.searchQuery = target.value;
    // TODO: Implement search functionality
    console.log('Search query:', this.searchQuery);
  }

  toggleSidebar() {
    this.sidebarCollapsed = !this.sidebarCollapsed;
  }

  private checkMobileView() {
    // On mobile, sidebar should be collapsed by default
    if (this.isBrowser && window.innerWidth <= 768) {
      this.sidebarCollapsed = true;
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    // Auto-collapse sidebar on mobile, auto-expand on desktop
    if (this.isBrowser) {
      if (event.target.innerWidth <= 768) {
        this.sidebarCollapsed = true;
      } else {
        this.sidebarCollapsed = false;
      }
    }
  }

  getUserInitials(): string {
    if (!this.currentUser) return 'U';
    const firstInitial = this.currentUser.firstName?.charAt(0)?.toUpperCase() || '';
    const lastInitial = this.currentUser.lastName?.charAt(0)?.toUpperCase() || '';
    return firstInitial + lastInitial || 'U';
  }

  getDefaultAvatar(): string {
    if (!this.currentUser) return 'U';
    // Return both initials (first + last name)
    const firstInitial = this.currentUser.firstName?.charAt(0)?.toUpperCase() || '';
    const lastInitial = this.currentUser.lastName?.charAt(0)?.toUpperCase() || '';
    return firstInitial + lastInitial || 'U';
  }

  getAvatarColor(): string {
    if (!this.currentUser) return '#6b7280';
    // Generate a consistent color based on user ID or name
    const colors = [
      '#2563eb', '#dc2626', '#16a34a', '#ca8a04', '#9333ea',
      '#c2410c', '#0891b2', '#be123c', '#4f46e5', '#059669'
    ];
    const index = (this.currentUser.id || this.currentUser.email).split('').reduce((a, b) => a + b.charCodeAt(0), 0) % colors.length;
    return colors[index];
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Logout error:', err);
        // Still clear local state even if backend fails
        this.router.navigate(['/']);
      }
    });
  }
}
