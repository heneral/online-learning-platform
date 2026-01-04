import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Dashboard } from './dashboard/dashboard';
import { AuthGuard } from './services/auth.guard';

export const routes: Routes = [
  { path: '', component: Home },
  {
    path: 'dashboard',
    component: Dashboard,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' },
      { path: 'overview', loadComponent: () => import('./dashboard/overview/overview').then(m => m.Overview) },
      { path: 'courses', loadComponent: () => import('./dashboard/courses/courses').then(m => m.Courses) },
      { path: 'users', loadComponent: () => import('./dashboard/users/users').then(m => m.Users) },
      { path: 'achievements', loadComponent: () => import('./dashboard/achievements/achievements').then(m => m.Achievements) },
      { path: 'lessons', loadComponent: () => import('./dashboard/lessons/lessons').then(m => m.Lessons) },
      { path: 'quizzes', loadComponent: () => import('./dashboard/quizzes/quizzes').then(m => m.Quizzes) }
    ]
  },
  { path: '**', redirectTo: '' }
];
