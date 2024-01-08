import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable} from 'rxjs';
import { Router } from '@angular/router';
import { Login } from './login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  result: any;


  constructor(private http:HttpClient,private router:Router) { }

  Login(userId : string,Password:string)
  {
    
    let user=new Login();
    user.UserId=userId;
    user.Password=Password;
    this.result=this.http.post('https://localhost:7209/api/Auth/login',user);
    return this.result;
  }

  

  public isLoggedIn(){
    return this.result !== null;
  }

  public logout(){
    if(!this.isLoggedIn()) return;
    localStorage.clear();
  }
}
