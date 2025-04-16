import { CanActivateFn, Router } from '@angular/router';
import { KeyStorage } from '../enums/storage.enums';
import { inject } from '@angular/core';

export const checkLoginGuard: CanActivateFn = (route, state) => {
  let user_id: any = localStorage.getItem(KeyStorage.user_id);
  const router = inject(Router);
  if (user_id === null) {
    router.navigate(['/auth']);
    return false;
  }
  return true;
};
