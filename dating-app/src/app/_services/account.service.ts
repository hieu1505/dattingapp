import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, pipe } from 'rxjs';
import { AuthUser, RegisterUser, UserToken } from '../_models/app-user';
 
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl: string = 'https://localhost:7069/api/Auth/';
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  private currentUser = new BehaviorSubject<UserToken | null>(null);
  currentUser$ = this.currentUser.asObservable();
 
  constructor(private httpClient: HttpClient) {}
 
  login(authUser: AuthUser): Observable<any> {
    return this.httpClient
      .post(`${this.baseUrl}login`, authUser, {
        responseType: 'text',
        headers: this.headers,
      })
      .pipe(
        map((token) => {
          if (token) {
            const userToken: UserToken = { username: authUser.username, token };
            this.currentUser.next(userToken);
            localStorage.setItem('userToken', JSON.stringify(userToken));
          }
        })
      );
  }
 
  logout() {
    this.currentUser.next(null);
    localStorage.removeItem('userToken');
  }
 
  reLogin() {
    const storageUser = localStorage.getItem('userToken');
    if (storageUser) {
      const currentUser = JSON.parse(storageUser);
      this.currentUser.next(currentUser);
    }
  }
 
  register(RegisterUser:RegisterUser) {
    return this.httpClient
    .post(`${this.baseUrl}register`, RegisterUser, {
      responseType: 'text',
      headers: this.headers,
    })
    .pipe(
      map((token) => {
        if (token) {
          const userToken: UserToken = { username: RegisterUser.username, token };
          localStorage.setItem('userToken', JSON.stringify(userToken));
          this.currentUser.next(userToken);
        }
      })
    );
  }
}
