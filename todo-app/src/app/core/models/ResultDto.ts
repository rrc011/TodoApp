import { IResultDto } from "../interfaces/IResultDto";

export class ResultDto<T> implements IResultDto<T> {
    messages: any[];
    succeeded: boolean;
    data: T;
    code: number;

    constructor(data?: IResultDto<T>) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.messages = _data["messages"];
            this.succeeded = _data["succeeded"];
            this.data = _data["data"];
            this.code = _data["code"];
        }
    }

}