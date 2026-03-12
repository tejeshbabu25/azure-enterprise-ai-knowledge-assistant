import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../config/api.config';
import { AskRequest } from '../models/ask-request';
import { AskResponse } from '../models/ask-response';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private readonly http = inject(HttpClient);

  ask(question: string): Observable<AskResponse> {
    const payload: AskRequest = { question };
    return this.http.post<AskResponse>(`${API_CONFIG.chatUrl}/ask`, payload);
  }
}