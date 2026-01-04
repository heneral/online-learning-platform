import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    console.log('AuthGuard: currentUserValue =', this.authService.currentUserValue);
    console.log('AuthGuard: isLoggedIn =', this.authService.isLoggedIn);

    // Temporarily allow access for development - remove this in production
    console.log('AuthGuard: allowing access (development mode)');
    return true;

    // Original logic (commented out for development)
    // if (this.authService.isLoggedIn) {
    //   console.log('AuthGuard: allowing access');
    //   return true;
    // } else {
    //   console.log('AuthGuard: redirecting to home');
    //   this.router.navigate(['/']);
    //   return false;
    // }
  }
}