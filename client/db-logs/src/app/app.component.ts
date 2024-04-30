import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'db-logs';

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    const userString = localStorage.getItem('employee');
    if (userString) {
      const user: User = JSON.parse(userString);
      this.accountService.setCurrentUser(user);
      const role = this.accountService.getRole();
      if (role === 'employee') {
        this.router.navigateByUrl('statistics');
      }
      else if (role === 'developer') {
        this.router.navigateByUrl('logs');
      }
    }
  }
}
