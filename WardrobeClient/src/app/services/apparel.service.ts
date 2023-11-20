import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Apparel } from '../models/apparel';

@Injectable({
  providedIn: 'root'
})
export class ApparelService {

  private readonly apiUrl = environment.apiUrl + 'Apparel';

  constructor(private http: HttpClient) { }


  getAll():Observable<Apparel[]>{
    return this.http.get<Apparel[]>(this.apiUrl);
  }

  delete(apparelId:number):Observable<Apparel>{
    return this.http.delete<Apparel>(this.apiUrl + '/' + apparelId);
  }

  create(apparel: Apparel): Observable<Apparel> {
    return this.http.post<Apparel>(this.apiUrl, apparel);
  }

  update(apparel: Apparel):Observable<Apparel> {
    return this.http.put<Apparel>(this.apiUrl + '/' + apparel.id, apparel);
  }

  findById(apparelId: number): Observable<Apparel>{
    return this.http.get<Apparel>(this.apiUrl + '/' + apparelId);
  }
}
