import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    if (this.authService.isLoggedIn) {
      const requiredRole = route.data['role'] as string;
      if (requiredRole && !this.authService.isInRole(requiredRole)) {
        this.router.navigate(['/']);
        return false;
      }
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
