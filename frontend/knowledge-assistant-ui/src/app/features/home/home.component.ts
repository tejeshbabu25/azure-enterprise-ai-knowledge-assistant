import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth/auth.service';
import { ProfileApiService, ProfileResponse } from '../../services/profile-api.services';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="p-4">
      <h1>Enterprise AI Knowledge Assistant</h1>

      <div class="mt-3">
        <button (click)="login()">Login</button>
        <button (click)="logout()">Logout</button>
        <button (click)="loadProfile()">Load Profile</button>
        <button routerLink="/chat">Go to Chat</button>
        <button routerLink="/admin">Go to Admin</button>
      </div>

      <p class="mt-3">Logged in: {{ auth.isLoggedIn() }}</p>
      <p>User: {{ auth.getUserName() }}</p>

      <pre *ngIf="profile">{{ profile | json }}</pre>
    </div>
  `
})
export class HomeComponent implements OnInit {
  auth = inject(AuthService);
  private readonly api = inject(ProfileApiService);

  profile?: ProfileResponse;

  ngOnInit(): void {
    if (!this.auth.isLoggedIn()) {
      this.login();
    }
  }

  login(): void {
    this.auth.login();
  }

  logout(): void {
    this.auth.logout();
  }

  loadProfile(): void {
    this.api.getMe().subscribe({
      next: res => this.profile = res,
      error: err => console.error('Profile load failed', err)
    });
  }
}