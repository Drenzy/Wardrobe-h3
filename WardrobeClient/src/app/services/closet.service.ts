import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Closet } from '../models/closet';

@Injectable({
  providedIn: 'root'
})
export class ClosetService {

  private readonly apiUrl = environment.apiUrl + 'Closet';

  constructor(private http: HttpClient) { }

  getAll():Observable<Closet[]>{
    return this.http.get<Closet[]>(this.apiUrl);
  }

  delete(closetId:number):Observable<Closet>{
    return this.http.delete<Closet>(this.apiUrl + '/' + closetId);
  }

  create(closet: Closet): Observable<Closet> {
    return this.http.post<Closet>(this.apiUrl, closet);
  }

    update(closet: Closet):Observable<Closet> {
      return this.http.put<Closet>(this.apiUrl + '/' + closet.id, closet);
    }

    findById(closetId: number): Observable<Closet>{
      return this.http.get<Closet>(this.apiUrl + '/' + closetId);
    }
}
