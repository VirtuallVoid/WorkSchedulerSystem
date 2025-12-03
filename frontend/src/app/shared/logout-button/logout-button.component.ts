import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-logout-button',
  standalone: true,
  imports: [CommonModule, MatIcon],
  template: `
    <button class="logout-btn" (click)="logout()">
      <mat-icon>logout</mat-icon>
    </button>
  `,
  styleUrls: ['./logout-button.component.scss']
})
export class LogoutButtonComponent {

  constructor(private auth: AuthService) {}

  logout() {
    this.auth.logout();
  }
}
