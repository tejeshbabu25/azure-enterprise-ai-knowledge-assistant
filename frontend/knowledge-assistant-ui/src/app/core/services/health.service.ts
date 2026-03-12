import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../config/api.config';

@Injectable({
  providedIn: 'root'
})
export class HealthService {
  private readonly http = inject(HttpClient);

  getHealth(): Observable<{ status: string; utcTime: string }> {
    return this.http.get<{ status: string; utcTime: string }>(API_CONFIG.healthUrl);
  }
}