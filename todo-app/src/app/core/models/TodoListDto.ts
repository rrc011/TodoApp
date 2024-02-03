import { ITodoListDto } from "../interfaces/ITodoListDto";
import { TodoItemDto } from "./TodoItemDto";

export class TodoListDto implements ITodoListDto {
    id?: number;
    title?: string | undefined;
    items?: TodoItemDto[];

    constructor(data?: ITodoListDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.title = _data["title"];
            if (Array.isArray(_data["items"])) {
                this.items = [] as any;
                for (let item of _data["items"])
                    this.items!.push(TodoItemDto.fromJS(item));
            }
        }
    }
}
