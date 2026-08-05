import { AuthService } from '@/app/services/authService';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  if(authService.isAutenticated()){
    return true;
  }
  router.navigate(['/login']);
  return false;
};
