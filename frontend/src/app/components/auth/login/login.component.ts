import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage = '';
  loading = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    this.authService.login(this.loginForm.value).subscribe({
      next: (response) => {
        const user = this.authService.currentUserValue;
        if (user?.roles.includes('Admin')) {
          this.router.navigate(['/admin/dashboard']);
        } else if (user?.roles.includes('Instructor')) {
          this.router.navigate(['/instructor/dashboard']);
        } else {
          this.router.navigate(['/student/dashboard']);
        }
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Invalid email or password';
        this.loading = false;
      }
    });
  }
}
