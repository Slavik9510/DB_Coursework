import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { LoginDto } from '../_models/login-dto.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {};
  constructor(public accountService: AccountService, private router: Router) { }

  login() {
    console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      }
    })
  }
}
