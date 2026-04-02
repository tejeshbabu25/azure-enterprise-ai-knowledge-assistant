import { ApplicationConfig, provideBrowserGlobalErrorListeners,importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient,withInterceptorsFromDi,HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { routes } from './app.routes';
import {
  MsalModule,
  MsalGuard,
  MsalInterceptor,
  MsalService,
  MsalBroadcastService,
  MSAL_INSTANCE,
  MSAL_GUARD_CONFIG,
  MSAL_INTERCEPTOR_CONFIG
} from '@azure/msal-angular';

import { msalInstanceFactory, msalGuardConfigFactory, msalInterceptorConfigFactory } from './auth/msal.config';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    importProvidersFrom(BrowserModule, MsalModule),
    { provide: MSAL_INSTANCE, useFactory: msalInstanceFactory },
    { provide: MSAL_GUARD_CONFIG, useFactory: msalGuardConfigFactory },
    { provide: MSAL_INTERCEPTOR_CONFIG, useFactory: msalInterceptorConfigFactory },
    MsalService,
    MsalGuard,
    MsalBroadcastService,
    { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true },
  ]
};
