import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      },
      error: error => {
        const firstErrorKey = Object.keys(error.error.errors)[0];
        const firstError = error.error.errors[firstErrorKey][0];
        console.log(firstError)
        if (error.error.errors)
          this.toastr.error(firstError);
        else
          this.toastr.error(error.error);
      }
    });
  }
}
