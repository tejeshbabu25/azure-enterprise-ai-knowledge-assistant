import { SourceItem } from './source-item';

export interface AskResponse {
  answer: string;
  sources: SourceItem[];
}