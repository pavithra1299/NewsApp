import { Component ,OnInit} from '@angular/core';
import {FormBuilder,FormGroup, Validators} from '@angular/forms';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';
import { LogService } from '../log.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errMsg : any;
  LoginForm : any;
  LoginId : any;
  Password : any;
  isLoggedin :any;


  constructor(private fb: FormBuilder, private loginService: LoginService,private router: Router,private logService:LogService) {}

  ngOnInit(): void {
      this.LoginForm = this.fb.group({
        UserName : ['',[
          Validators.required,
        ]   ],
        Password : ['',[
          Validators.required,
        ]],
      });
    
     
    
  }
  onSubmit(form : FormGroup) {
    if (form.invalid) {
      return;
    }
    
    this.LoginId=form.value.UserName ;
    this.Password=form.value.Password;

    this.loginService.Login(this.LoginId,this.Password).subscribe(
      (result: any) => {
        console.warn(result);
        sessionStorage.setItem('token',result.token);
        localStorage.setItem('userName',this.LoginId);
        this.isLoggedin=true;
        this.errMsg = 'Login Successfull ';
        window.alert('Login Successfull');
        this.logService.log('Login success!!');
        this.router.navigate(['/home']); 
      },
      (err: any) => {
        console.log(err);
        this.isLoggedin=false;
        this.errMsg = 'Please Fill all the required Fields ';
        this.logService.error('Login failed..');
        window.alert("Invalid credentials");
      }
    );
    form.reset();
   
  }

}
