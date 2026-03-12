import { environment } from '../../environments/environment';

export const API_CONFIG = {
  baseUrl: environment.apiBaseUrl,
  healthUrl: `${environment.apiBaseUrl}/health`,
  chatUrl: `${environment.apiBaseUrl}/chat`,
  adminUrl: `${environment.apiBaseUrl}/admin`
};