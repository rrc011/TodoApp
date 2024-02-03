export interface IResultDto<T> {
    messages: any[];
    succeeded: boolean;
    data: T;
    code: number;
}