import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormsModule,
  ReactiveFormsModule,
  FormGroup,
  Validators
} from '@angular/forms';

import { MatTabsModule } from '@angular/material/tabs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';

import { AuthService } from '../../../core/services/auth.service';
import { loginForm } from '../forms/login.form';
import { registerForm } from '../forms/register.form';
import { firstValueFrom } from 'rxjs';

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
  jobs: any[] = [];

  loading = false;
  errorMessages: string[] = [];

  constructor(private auth: AuthService, private cd: ChangeDetectorRef) {}

  async ngOnInit() {
    await this.loadJobs();

    this.registerForm.get('roleId')!.valueChanges.subscribe(role => {
      const jobControl = this.registerForm.get('jobId')!;

      if (role === 1) {
        jobControl.setValue(null);
        jobControl.clearValidators();
      } else {
        jobControl.setValidators([Validators.required]);
      }

      jobControl.updateValueAndValidity();
    });
  }

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
    }

    this.loading = false;
    this.cd.detectChanges();
  }

  async onRegister() {
    if (this.registerForm.invalid) return;

    this.loading = true;
    this.errorMessages = [];
    await Promise.resolve();
    this.cd.detectChanges();

    try {
      const form = this.registerForm.value;

      const dto = {
        fullName: form.fullName,
        userName: form.userName,
        password: form.password,
        roleId: Number(form.roleId),
        jobId: form.roleId === 1 ? null : Number(form.jobId)
      };

      await this.auth.register(dto);

      alert('Registration successful!');
      this.selectedTab = 0;

    } catch (e: any) {
      this.errorMessages = e.messages || ['Registration failed'];
    }

    this.loading = false;
    this.cd.detectChanges();
  }

  private async loadJobs() {
    try {
      const res = await firstValueFrom(this.auth.getJobs());

      this.jobs = (res.data || []).map((j: any) => ({
        id: Number(j.id),
        jobName: j.jobName
      }));

      this.cd.detectChanges();

    } catch (e) {
      console.error(e);
      this.errorMessages = ['Failed to load jobs from server'];
    }
  }
}
