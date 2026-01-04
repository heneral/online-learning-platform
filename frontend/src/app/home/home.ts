import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { LoginComponent } from '../components/auth/login/login.component';
import { RegisterComponent } from '../components/auth/register/register.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  imports: [CommonModule, LoginComponent, RegisterComponent],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit, OnDestroy {
  showAuthModal = false;
  authMode: 'login' | 'register' = 'login';
  isLoggedIn = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.currentUser$.subscribe(user => {
      this.isLoggedIn = !!user;
    });
  }

  ngOnDestroy() {
    // Cleanup if needed
  }

  @HostListener('document:keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    if (event.key === 'Escape' && this.showAuthModal) {
      this.closeModal();
    }
  }

  openLogin() {
    console.log('Opening login modal');
    this.authMode = 'login';
    this.showAuthModal = true;
    this.preventBodyScroll();
  }

  openRegister() {
    console.log('Opening register modal');
    this.authMode = 'register';
    this.showAuthModal = true;
    this.preventBodyScroll();
  }

  goToDashboard() {
    console.log('Going to dashboard');
    this.router.navigate(['/dashboard']);
  }

  closeModal() {
    console.log('Closing modal');
    this.showAuthModal = false;
    this.allowBodyScroll();
  }

  private preventBodyScroll() {
    document.body.style.overflow = 'hidden';
  }

  private allowBodyScroll() {
    document.body.style.overflow = '';
  }

  onLoginSuccess() {
    console.log('Login success, closing modal and navigating');
    this.closeModal();
    this.router.navigate(['/dashboard']);
  }

  onRegisterSuccess() {
    console.log('Register success, closing modal and navigating');
    this.closeModal();
    this.router.navigate(['/dashboard']);
  }

  switchToRegister() {
    this.authMode = 'register';
  }

  switchToLogin() {
    this.authMode = 'login';
  }

  scrollToCourses() {
    const element = document.getElementById('courses');
    element?.scrollIntoView({ behavior: 'smooth' });
  }
}
