import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { ChatService } from '../../core/services/chat.service';
import { AskResponse } from '../../core/models/ask-response';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {
  private readonly chatService = inject(ChatService);

  question = '';
  loading = false;
  error = '';
  result: AskResponse | null = null;

  ask(): void {
    const trimmed = this.question.trim();
    if (!trimmed || this.loading) {
      return;
    }

    this.loading = true;
    this.error = '';
    this.result = null;

    this.chatService.ask(trimmed)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe({
        next: (response) => {
          this.result = response;
        },
        error: (err) => {
          console.error('Chat request failed', err);
          this.error = 'Unable to get an answer right now. Check whether the API is running and the index is loaded.';
        }
      });
  }

  loadSampleQuestion(question: string): void {
    this.question = question;
  }
}