import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment.development';
import { Observable } from 'rxjs';

export interface ProfileResponse {
  isAuthenticated: boolean;
  objectId: string | null;
  tenantId: string | null;
  userPrincipalName: string | null;
  email: string | null;
  lanId: string | null;
  groupIds: string[];
  isAdmin: boolean;
  isReadOnlyUser: boolean;
}

@Injectable({ providedIn: 'root' })
export class ProfileApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.api.baseUrl;

  getMe(): Observable<ProfileResponse> {
    return this.http.get<ProfileResponse>(`${this.baseUrl}/profile/me`);
  }

  getReadData(): Observable<unknown> {
    return this.http.get(`${this.baseUrl}/profile/read`);
  }

  callAdmin(): Observable<unknown> {
    return this.http.post(`${this.baseUrl}/profile/admin`, {});
  }
}