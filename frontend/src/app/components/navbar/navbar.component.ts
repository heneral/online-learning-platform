import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/models';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  currentUser: User | null = null;

  constructor(public authService: AuthService) {
    this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
  }

  logout(): void {
    this.authService.logout();
  }
}
