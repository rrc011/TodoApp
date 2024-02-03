import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { TodoItemDto } from '../core/models/TodoItemDto';

@Injectable({
  providedIn: 'root'
})
export class TodoItemService extends BaseService<TodoItemDto, number> {
  constructor(_http: HttpClient) {
    super(_http, "/api/TodoItem");
  }
}
