import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { AdminService } from '../../core/services/admin.service';
import { HealthService } from '../../core/services/health.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  private readonly adminService = inject(AdminService);
  private readonly healthService = inject(HealthService);

  folderPath = '';
  setupMessage = '';
  ingestMessage = '';
  healthMessage = '';
  loadingSetup = false;
  loadingIngest = false;
  loadingHealth = false;

  checkHealth(): void {
    this.loadingHealth = true;
    this.healthMessage = '';

    this.healthService.getHealth()
      .pipe(finalize(() => (this.loadingHealth = false)))
      .subscribe({
        next: (result) => {
          this.healthMessage = `${result.status} - ${result.utcTime}`;
        },
        error: () => {
          this.healthMessage = 'API health check failed.';
        }
      });
  }

  setupIndex(): void {
    this.loadingSetup = true;
    this.setupMessage = '';

    this.adminService.setup()
      .pipe(finalize(() => (this.loadingSetup = false)))
      .subscribe({
        next: (result) => {
          this.setupMessage = result.message;
        },
        error: (err) => {
          console.error(err);
          this.setupMessage = 'Failed to create or update the search index.';
        }
      });
  }

  ingestDocuments(): void {
    this.loadingIngest = true;
    this.ingestMessage = '';

    const folder = this.folderPath.trim() || undefined;

    this.adminService.ingest(folder)
      .pipe(finalize(() => (this.loadingIngest = false)))
      .subscribe({
        next: (result) => {
          this.ingestMessage = result.message;
        },
        error: (err) => {
          console.error(err);
          this.ingestMessage = 'Document ingestion failed.';
        }
      });
  }
}