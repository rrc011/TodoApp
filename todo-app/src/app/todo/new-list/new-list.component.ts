import { Component, EventEmitter, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { TodoListDto } from 'src/app/core/models/TodoListDto';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-new-list',
  templateUrl: './new-list.component.html',
  styleUrls: ['./new-list.component.css']
})
export class NewListComponent implements OnInit {
  public subscription: Subscription = new Subscription()
  public onSave: EventEmitter<any> = new EventEmitter<any>()
  public todoList: TodoListDto = new TodoListDto();
  title: string = "New List"
  textButton: string = "Create List"
  isEdit: boolean = false

  constructor(private activeModal: NgbActiveModal, private todoService: TodoService) { }

  ngOnInit() {
    if (this.todoList.id) {
      this.isEdit = true
      this.title = "Edit List"
      this.textButton = "Edit List"
    }
  }

  close(): void {
    this.activeModal.close()
  }

  submit() {
    if (this.todoList.id) {
      this.update()
    } else {
      this.save()
    }
  }

  save() {
    this.subscription.add(
      this.todoService.post(this.todoList).subscribe(
        () => {
          this.todoList = new TodoListDto()
          this.onSave.emit()
          this.close()
        },
        error => {
          console.error(error)
        }
      )
    )
  }

  update() {
    const payload = new TodoListDto();
    payload.id = this.todoList.id;
    payload.title = this.todoList.title;

    this.subscription.add(
      this.todoService.put(payload, this.todoList.id as number).subscribe(
        () => {
          this.todoList = new TodoListDto()
          this.onSave.emit()
          this.close()
        },
        error => {
          console.error(error)
        }
      )
    )
  }
  deleteHandler() {
    this.subscription.add(
      this.todoService.delete(this.todoList.id as number).subscribe(
        () => {
          this.todoList = new TodoListDto()
          this.onSave.emit()
          this.close()
        },
        error => {
          console.error(error)
        }
      )
    )
  }
}
