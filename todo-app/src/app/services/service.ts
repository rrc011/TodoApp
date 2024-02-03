import { Observable } from "rxjs";

export interface IService<TEntity, TKey> {
    get(url: string, data: any): Observable<any>;
    getAll(): Observable<any[]>;
    getById(id: TKey): Observable<any>;
    post(entity: TEntity): Observable<any>;
    put(entity: TEntity, id: TKey): Observable<any>;
    delete(id: TKey): Observable<TEntity>;
}
