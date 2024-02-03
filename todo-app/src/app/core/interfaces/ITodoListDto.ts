import { TodoItemDto } from "../models/TodoItemDto";

export interface ITodoListDto {
    id?: number;
    title?: string | undefined;
    colour?: string | undefined;
    items?: TodoItemDto[];
}
