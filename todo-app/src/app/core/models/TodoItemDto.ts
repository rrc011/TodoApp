import { ITodoItemDto } from "../interfaces/ITodoItemDto";

export class TodoItemDto implements ITodoItemDto {
    id?: number;
    listId?: number;
    description?: string | undefined;
    done?: boolean;

    constructor(data?: ITodoItemDto) {
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
            this.listId = _data["listId"];
            this.description = _data["description"];
            this.done = _data["done"];
        }
    }

    static fromJS(data: any): TodoItemDto {
        data = typeof data === 'object' ? data : {};
        let result = new TodoItemDto();
        result.init(data);
        return result;
    }

}
