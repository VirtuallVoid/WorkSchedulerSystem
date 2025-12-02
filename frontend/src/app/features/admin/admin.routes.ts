// features/admin/admin.routes.ts
import { Routes } from '@angular/router';
import { AdminPage } from './pages/admin-dashboard.page';

export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    component: AdminPage,
  }
];
