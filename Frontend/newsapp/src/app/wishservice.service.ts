import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map,Observable } from 'rxjs';
import { Wish } from './wish';

@Injectable({
  providedIn: 'root'
})
export class WishserviceService {
  result:any;
  userName = localStorage.getItem('userName');
  
  constructor(private http:HttpClient) { }

  AddToWishlist(FavouriteNews:any,userName : any):Observable<any>{
  
    this.result=this.http.post("https://localhost:7179/api/Wishlist?userName="+userName,FavouriteNews);

    return this.result;
  }

   GetWishlist(userName:any):Observable<any>{
    
     console.log(userName);
     return this.http.get('https://localhost:7179/api/Wishlist?userName='+userName);
   }

   DeleteFromWishlist(userName:any,author : any):Observable<any>
   {
      return this.http.delete('https://localhost:7179/api/Wishlist/'+author+'?userName='+userName);
   }


}

