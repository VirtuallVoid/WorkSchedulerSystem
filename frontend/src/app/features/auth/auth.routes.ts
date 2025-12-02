import { Routes } from '@angular/router';
import { AuthPage } from './pages/auth.page';

export const AUTH_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/auth.page').then(m => m.AuthPage)
  },
];
