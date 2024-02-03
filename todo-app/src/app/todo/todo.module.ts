import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoComponent } from './todo.component';
import { FormsModule } from '@angular/forms';
import { NewListComponent } from './new-list/new-list.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
  ],
  declarations: [TodoComponent, NewListComponent],
  entryComponents: [NewListComponent],
  exports: [TodoComponent]
})
export class TodoModule { }
