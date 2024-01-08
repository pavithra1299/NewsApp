import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map,Observable } from 'rxjs';
import { User } from './user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {
  result: any;

  constructor(private http:HttpClient,private router:Router) { }


  Register(user: User) {
    console.warn(user);
    this.result = this.http.post(
      'https://localhost:7172/api/user', user
    );
    return this.result;
  }

 
}
