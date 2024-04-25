import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationClient } from '../clients/authentication.client';
import { AlertService } from './alert.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private tokenTypeKey = 'tokentype'
  private tokenKey = 'token';
  private refreshKey = 'refreshtoken';
  private expiresInKey = 'expiresin';

  constructor(
    private authenticationClient: AuthenticationClient,
    private router: Router,
    private alertService: AlertService
  ) {}

  public login(username: string, password: string): void {
    this.authenticationClient.login(username, password).subscribe((res) => {
        const { accessToken, tokenType, expiresIn, refreshToken } = JSON.parse(res);
        localStorage.setItem(this.tokenKey, accessToken);
        localStorage.setItem(this.tokenTypeKey, tokenType);
        localStorage.setItem(this.expiresInKey, expiresIn);
        localStorage.setItem(this.refreshKey, refreshToken);
        this.router.navigate(['/']);
      },
      (err) => {
        this.alertService.openSnackBar(`Login error: ${err.error}`);
      }
    );
  }

  public register(email: string, password: string): void {
    this.authenticationClient
      .register(email, password)
      .subscribe((res) => {
        this.alertService.openSnackBar(`The user ${email} is successfully registered`);
      },
      (err) => {
        this.alertService.openSnackBar(`Register error: ${err.error}`);
      }
    );
  }

  public logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  public isLoggedIn(): boolean {
    let token = localStorage.getItem(this.tokenKey);
    return token != null && token.length > 0;
  }

  public getToken(): string | null {
    return this.isLoggedIn() ? localStorage.getItem(this.tokenKey) : null;
  }

  public getCurrentUserInfo(): Observable<any>{
    return this.authenticationClient.getCurrentUserInfo()
  }
  
}
