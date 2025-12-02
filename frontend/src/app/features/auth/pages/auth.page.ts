import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { AuthService } from '../../../core/services/auth.service';
import { loginForm } from '../forms/login.form';
import { registerForm } from '../forms/register.form';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
  ],
  templateUrl: './auth.page.html',
  styleUrls: ['./auth.page.scss'],
})
export class AuthPage {

  selectedTab = 0;
  loginForm: FormGroup = loginForm;
  registerForm: FormGroup = registerForm;

  loading = false;
  errorMessages: string[] = [];

  constructor(private auth: AuthService, private cd: ChangeDetectorRef) {}

  async onLogin() {
    if (this.loginForm.invalid) return;

    this.loading = true;
    this.errorMessages = [];
    await Promise.resolve();
    this.cd.detectChanges();

    try {
      const { userName, password } = this.loginForm.value;
      await this.auth.login(userName, password);

      const role = this.auth.getRole();
      window.location.href = role === 'Admin' ? '/admin' : '/worker';

    } catch (e: any) {
      this.errorMessages = e.messages || ['Login failed'];
      await Promise.resolve();
      this.cd.detectChanges();
    }

    this.loading = false;
    await Promise.resolve();
    this.cd.detectChanges();
  }

  async onRegister() {
    if (this.registerForm.invalid) return;

    this.loading = true;
    this.errorMessages = [];
    await Promise.resolve();
    this.cd.detectChanges();

    try {
      const dto = this.registerForm.value;
      await this.auth.register(dto);

      alert('Registration successful!');
      this.selectedTab = 0;

      await Promise.resolve();
      this.cd.detectChanges();

    } catch (e: any) {
      this.errorMessages = e.messages || ['Registration failed'];
      await Promise.resolve();
      this.cd.detectChanges();
    }

    this.loading = false;
    await Promise.resolve();
    this.cd.detectChanges();
  }
}
