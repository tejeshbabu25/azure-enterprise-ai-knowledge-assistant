import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  template: `
    <header class="topbar">
      <div class="brand">Enterprise AI Knowledge Assistant</div>

      <nav class="nav">
        <a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{ exact: true }">
          Chat
        </a>
        <a routerLink="/admin" routerLinkActive="active">
          Admin
        </a>
      </nav>
    </header>
  `,
  styles: [`
    .topbar {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 16px 24px;
      border-bottom: 1px solid #e5e7eb;
      background: #ffffff;
      position: sticky;
      top: 0;
      z-index: 10;
    }

    .brand {
      font-size: 18px;
      font-weight: 600;
    }

    .nav {
      display: flex;
      gap: 16px;
    }

    .nav a {
      text-decoration: none;
      color: #374151;
      font-weight: 500;
    }

    .nav a.active {
      color: #111827;
      border-bottom: 2px solid #111827;
      padding-bottom: 4px;
    }
  `]
})
export class TopbarComponent {}