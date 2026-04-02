import {
  IPublicClientApplication,
  PublicClientApplication,
  BrowserCacheLocation,
  InteractionType,
  LogLevel
} from '@azure/msal-browser';
import {
  MsalGuardConfiguration,
  MsalInterceptorConfiguration
} from '@azure/msal-angular';
import { environment } from '../environments/environment';

export function loggerCallback(logLevel: LogLevel, message: string): void {
  // Avoid logging tokens or PII
  console.log(`[MSAL][${LogLevel[logLevel]}] ${message}`);
}

export function msalInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.azureAd.clientId,
      authority: environment.azureAd.authority,
      redirectUri: environment.azureAd.redirectUri,
      postLogoutRedirectUri: environment.azureAd.postLogoutRedirectUri
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage
    },
    system: {
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false
      }
    }
  });
}

export function msalGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: ['openid', 'profile', 'offline_access', environment.api.scope]
    },
    loginFailedRoute: '/auth-failed'
  };
}

export function msalInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, string[]>();

  // Use exact path or a wildcard pattern that matches your real API calls.
  protectedResourceMap.set(`${environment.api.baseUrl}/*`, [environment.api.scope]);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}