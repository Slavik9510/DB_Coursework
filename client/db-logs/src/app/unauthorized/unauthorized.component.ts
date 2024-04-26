import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css']
})
export class UnauthorizedComponent {

  constructor(private router: Router, private accountService: AccountService) { }

  goBack() {
    const role = this.accountService.getRole();
    if (role === 'employee') {
      this.router.navigateByUrl('statistics')
    }
    else if (role === 'developer') {
      this.router.navigateByUrl('logs')
    }
  }
}
