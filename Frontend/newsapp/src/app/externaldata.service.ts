import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map,Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ExternaldataService {

  constructor(private http:HttpClient) { }
 
 
  getNews(search : string): Observable<any> {
    
    return this.http.get<any>("https://localhost:7189/api/News/"+search)
    .pipe(map((res:any)=>{
      return res;
    }))
  }
}
