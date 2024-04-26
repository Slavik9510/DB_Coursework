import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../_models/login-dto.model';
import { User } from '../_models/user.model';
import { BehaviorSubject, map } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7272/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(model: LoginDto) {
    return this.http.post<User>(this.baseUrl + 'account/login-employee', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  getRole() {
    const currentUser = this.currentUserSource.getValue();
    if (currentUser && currentUser.token) {
      const decodedToken = this.decodeToken(currentUser.token);
      return decodedToken ? decodedToken.role : null;
    }
    return null;
  }

  private decodeToken(token: string): any {
    try {
      return this.jwtHelper.decodeToken(token);
    } catch (Error) {
      console.error('Error decoding token', Error);
      return null;
    }
  }

  setCurrentUser(user: User) {
    localStorage.setItem('employee', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('employee');
    this.currentUserSource.next(null);
  }
}
