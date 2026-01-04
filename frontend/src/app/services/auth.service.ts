import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map, finalize } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';
import { User, AuthResponse, AuthResponseDto, LoginRequest, RegisterRequest } from '../models/models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Check if user is stored in localStorage (only in browser)
    if (isPlatformBrowser(this.platformId)) {
      const storedUser = localStorage.getItem('currentUser');
      if (storedUser) {
        try {
          this.currentUserSubject.next(JSON.parse(storedUser));
        } catch (e) {
          // Invalid JSON, clear it
          localStorage.removeItem('currentUser');
          localStorage.removeItem('token');
        }
      }
    }
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  public get isLoggedIn(): boolean {
    return !!this.currentUserValue;
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/login`, credentials)
      .pipe(map(response => {
        const user: User = {
          id: response.userId,
          firstName: response.firstName,
          lastName: response.lastName,
          email: response.email,
          roles: response.roles,
          avatarUrl: response.avatarUrl
        };
        // Store user details and jwt token in local storage (only in browser)
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          localStorage.setItem('token', response.token);
        }
        console.log('User set in auth service:', user);
        this.currentUserSubject.next(user);
        return { user, token: response.token };
      }));
  }

  register(data: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/register`, data)
      .pipe(map(response => {
        const user: User = {
          id: response.userId,
          firstName: response.firstName,
          lastName: response.lastName,
          email: response.email,
          roles: response.roles,
          avatarUrl: response.avatarUrl
        };
        // Store user details and jwt token in local storage (only in browser)
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          localStorage.setItem('token', response.token);
        }
        this.currentUserSubject.next(user);
        return { user, token: response.token };
      }));
  }

  logout(): Observable<any> {
    // Call backend logout endpoint
    return this.http.post(`${this.apiUrl}/logout`, {}).pipe(
      map(() => {
        // Backend logout successful
      }),
      finalize(() => {
        // Always clear local state regardless of backend response
        if (isPlatformBrowser(this.platformId)) {
          localStorage.removeItem('currentUser');
          localStorage.removeItem('token');
        }
        this.currentUserSubject.next(null);
      })
    );
  }
}
