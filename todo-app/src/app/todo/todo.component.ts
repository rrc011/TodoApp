import { Component, OnInit, NgZone } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

import { TodoListDto } from '../core/models/TodoListDto';
import { TodoItemDto } from '../core/models/TodoItemDto';
import { TodoService } from '../services/todo.service';
import { NewListComponent } from './new-list/new-list.component';
import { TodoItemService } from '../services/todo-item.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {
  public subscription: Subscription = new Subscription();

  lists: TodoListDto[] = [];
  selectedList: TodoListDto = new TodoListDto();
  selectedItem: TodoItemDto = new TodoItemDto();

  constructor(
    private modalService: NgbModal,
    private todoService: TodoService,
    private todoItemService: TodoItemService,
    private ngZone: NgZone,
    private toastr: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.refresh()
  }

  refresh(): void {
    this.todoService.getAll().subscribe((data: any) => {
      this.lists = data.data;
      if (this.lists.length > 0) {
        this.selectedList = this.lists[0];
      }
    });
  }

  showNewListModal(): void {
    let modal: NgbModalRef = this.modalService.open(NewListComponent);
    this.subscription.add(modal.componentInstance.onSave.subscribe(() => {
      this.toastr.success('List created successfully.');
      this.refresh()
    }));
  }

  remainingItems(list: TodoListDto): number {
    return list.items ? list.items.filter(t => !t.done).length : 0;
  }

  showListOptionsModal(list: TodoListDto): void {
    let modal: NgbModalRef = this.modalService.open(NewListComponent);
    Object.assign(modal.componentInstance.todoList, list);
    this.subscription.add(modal.componentInstance.onSave.subscribe(() => {
      this.toastr.success('List updated successfully.');
      this.refresh()
    }));
  }

  addTempItem() {
    const item = new TodoItemDto();

    item.init({
      id: 0,
      listId: this.selectedList.id,
      title: '',
      done: false
    });

    if (this.selectedList.items !== undefined) {
      this.selectedList.items.push(item);
      const index = this.selectedList.items.length - 1;
      this.focusItem(item, 'itemTitle' + index);
    }
  }

  focusItem(item: TodoItemDto, inputId: string): void {
    this.selectedItem = item;
    this.ngZone.runOutsideAngular(() => {
      setTimeout(() => {
        const element = document.getElementById(inputId);
        if (element) {
          element.focus();
        }
      }, 0);
    });
  }

  saveItem(item: TodoItemDto, pressedEnter: boolean = false): void {
    const isNewItem = item.id === 0;

    if (!item.description?.trim()) {
      this.deleteItem(item.id);
      return;
    }

    if (item.id === 0) {
      this.todoItemService
        .post({ description: item.description, listId: this.selectedList.id } as TodoItemDto)
        .subscribe({
          next: (result: any) => {
            item.id = result.data;
            this.toastr.success('Item created successfully.');
          },
          error: (error) => console.error(error),
        });
    } else {
      this.todoItemService.put(item as TodoItemDto, item.id as number).subscribe({
        next: () => {
          this.toastr.success('Item updated successfully.');
        },
        error: (error) => console.error(error),
      });
    }

    this.selectedItem = new TodoItemDto();

    if (isNewItem && pressedEnter) {
      setTimeout(() => this.addTempItem(), 250);
    }
  }

  deleteItem(id: number) {
    if (id === 0) {
      const itemIndex = this.selectedList.items?.indexOf(this.selectedItem);
      if (itemIndex !== undefined) {
        this.selectedList.items?.splice(itemIndex, 1);
      }
    } else {
      this.todoItemService.delete(id).subscribe({
        next: () => {
          this.refresh();
          this.toastr.success('Item deleted successfully.');
        },
        error: (error) => console.error(error),
      });
    }
  }
}
