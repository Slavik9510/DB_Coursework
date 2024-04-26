import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const statisticsGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  if (accountService.getRole() === 'employee') {
    return true;
  }

  router.navigate(['/unauthorized']);
  return false;
};
