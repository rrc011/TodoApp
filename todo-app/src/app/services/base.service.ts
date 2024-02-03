import { Inject, Injectable } from '@angular/core';
import { IService } from "./service"
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BaseService<TEntity, TKey> implements IService<TEntity, TKey> {
  baseUrl = "https://localhost:7272" + this._baseUrl;

  constructor(@Inject(HttpClient) protected _http: HttpClient, @Inject(String) private _baseUrl: string) { }

  protected getHeaders(contentType: string = "") {
    const headers = new HttpHeaders({
      "Content-Type": contentType === "" ? "application/json" : contentType,
    });
    return { headers: headers };
  }

  get(url: string, data: any): Observable<any> {
    let datos: any = this.getHeaders();
    datos["params"] = data;
    return this._http.get<TEntity>(url, datos);
  }
  getAll(): Observable<any[]> {
    return this._http.get<TEntity[]>(`${this.baseUrl}`, this.getHeaders());
  }
  getById(id: TKey): Observable<TEntity> {
    return this._http.get<TEntity>(`${this.baseUrl}?Id=${id}`, this.getHeaders());
  }
  post(entity: TEntity): Observable<number> {
    return this._http
      .post<number>(`${this.baseUrl}`, entity, this.getHeaders());
  }
  put(entity: TEntity, id: TKey): Observable<TEntity> {
    return this._http.put<TEntity>(`${this.baseUrl}`, entity, this.getHeaders());
  }
  delete(id: TKey): Observable<TEntity> {
    return this._http.delete<TEntity>(
      `${this.baseUrl}?id=${id}`,
      this.getHeaders()
    );
  }
}
