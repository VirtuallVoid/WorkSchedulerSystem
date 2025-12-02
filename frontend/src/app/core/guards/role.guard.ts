import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export function roleGuard(expectedRole: 'Admin' | 'Worker'): CanActivateFn {
  return () => {
    const router = inject(Router);
    const role = localStorage.getItem('role');

    if (role !== expectedRole) {
      router.navigate(['/auth']);
      return false;
    }

    return true;
  };
}
