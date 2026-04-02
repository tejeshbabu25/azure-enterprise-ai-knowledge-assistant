import { environment } from '../../environments/environment';

export const API_CONFIG = {
  baseUrl: environment.api.baseUrl,
  healthUrl: `${environment.api.baseUrl}/health`,
  chatUrl: `${environment.api.baseUrl}/chat`,
  adminUrl: `${environment.api.baseUrl}/admin`
};