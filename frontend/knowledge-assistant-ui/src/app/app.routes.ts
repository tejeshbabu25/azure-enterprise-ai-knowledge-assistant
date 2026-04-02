import { Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './features/home/home.component';
import { ChatComponent } from './features/chat/chat.component';
import { AdminComponent } from './features/admin/admin.component';

export const routes: Routes = [
  { path: '', component: HomeComponent , canActivate: [MsalGuard]},
  { path: 'chat', component: ChatComponent , canActivate: [MsalGuard]},
  { path: 'admin', component: AdminComponent , canActivate: [MsalGuard]}
];