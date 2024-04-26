import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginDto: any = {};

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  login() {
    this.accountService.login(this.loginDto).subscribe({
      next: () => {
        const role = this.accountService.getRole();
        if (role === 'employee') {
          this.router.navigateByUrl('statistics');
        }
        else if (role === 'developer') {
          this.router.navigateByUrl('logs');
        }
      },
      error: error => {
        this.toastr.error(error.error);
      }
    })
  }
}
