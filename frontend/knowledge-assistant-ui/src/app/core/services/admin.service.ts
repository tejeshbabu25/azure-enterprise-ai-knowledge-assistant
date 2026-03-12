import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../config/api.config';
import { OperationResult } from '../models/ingest-response';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly http = inject(HttpClient);

  setup(): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${API_CONFIG.adminUrl}/setup`, {});
  }

  ingest(folderPath?: string): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${API_CONFIG.adminUrl}/ingest`, {
      folderPath: folderPath ?? null
    });
  }
}