import { Routes } from '@angular/router';
import { ChatComponent } from './features/chat/chat.component';
import { AdminComponent } from './features/admin/admin.component';

export const routes: Routes = [
  { path: '', component: ChatComponent },
  { path: 'admin', component: AdminComponent },
  { path: '**', redirectTo: '' }
];