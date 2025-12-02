import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const appRoutes: Routes = [

  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes')
        .then(m => m.AUTH_ROUTES)
  },

  {
    path: 'admin',
    canActivate: [authGuard, roleGuard('Admin')],
    loadChildren: () =>
      import('./features/admin/admin.routes')
        .then(m => m.ADMIN_ROUTES)
  },

  {
    path: 'worker',
    canActivate: [authGuard, roleGuard('Worker')],
    loadChildren: () =>
      import('./features/worker/worker.routes')
        .then(m => m.WORKER_ROUTES)
  },

  { path: '**', redirectTo: 'auth' }
];
