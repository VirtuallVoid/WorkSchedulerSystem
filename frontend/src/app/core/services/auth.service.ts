import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import {ApiResponse} from '../../shared/models/api-response.model';
@Injectable({ providedIn: 'root' })
export class AuthService {

  private api = environment.apiUrl;

  constructor(private http: HttpClient,private router: Router) {}

  async login(userName: string, password: string) {
    try {
      const response: any = await this.http
        .post(`${this.api}/auth/login`, { userName, password })
        .toPromise();

      if (response.succeeded === true) {
        localStorage.setItem('token', response.data.bearerToken);
        localStorage.setItem('role', response.data.role);
        localStorage.setItem('userName', response.data.userName);
        localStorage.setItem('expiry', response.data.tokenExpirationDate);
        localStorage.setItem('userId', response.data.userId.toString());
        return;
      }

      throw response;

    } catch (err: any) {
      const backend = err?.error ?? err;

      if (backend.code === 400 && backend.validationErrors) {
        const list = Object.values(backend.validationErrors).flat();
        throw { type: 'validation', messages: list };
      }

      if (backend.code === 401 || backend.error === 'INVALID_CREDENTIALS') {
        throw { type: 'credentials', messages: ['Invalid username or password.'] };
      }

      throw { type: 'unknown', messages: ['Unexpected error occurred.'] };
    }
  }

  async register(dto: any) {
    try {
      const response: any = await this.http
        .post(`${this.api}/auth/register`, dto)
        .toPromise();

      if (response.succeeded === true) {
        return response;
      }
      throw response;

    } catch (err: any) {
      const backend = err?.error ?? err;

      if (backend.code === 400 && backend.validationErrors) {
        const list = Object.values(backend.validationErrors).flat();
        throw { type: 'validation', messages: list };
      }

      if (backend.code === 401 || backend.error === 'USER_EXISTS') {
        throw { type: 'duplicate', messages: [backend.message] };
      }

      throw { type: 'unknown', messages: ['Unexpected error occurred.'] };
    }
  }

  getJobs() {
    return this.http.get<ApiResponse<any[]>>(`${this.api}/schedules/jobs`);
  }

  getRole() {
    return localStorage.getItem('role');
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth']);
  }

}
