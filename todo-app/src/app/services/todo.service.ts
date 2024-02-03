import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoListDto } from '../core/models/TodoListDto';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class TodoService extends BaseService<TodoListDto, number> {
  constructor(_http: HttpClient) {
    super(_http, "/api/TodoList");
  }
}
