import { Routes } from '@angular/router';

export const WORKER_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/worker-dashboard.page').then(m => m.WorkerPage)
  }
];
