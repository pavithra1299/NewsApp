import { Component, OnInit } from '@angular/core';
import {FormBuilder,FormGroup, Validators} from '@angular/forms';
import { UserServiceService } from '../user-service.service';
import { User } from '../user';
import { Router } from '@angular/router';
import { LogService } from '../log.service';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit  {

  errMsg: any;
  LoggedInCheack = false;
  myForm: any;
  private _router: any;
 
  constructor(private fb: FormBuilder, private logService:LogService,private userService: UserServiceService , private router:Router) {}

  ngOnInit(): void {
    this.myForm = this.fb.group({
      
      UserName: [ '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(10),
        ],
      ],
      FirstName: [ '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(25),
        ],
      ],
      LastName: [ '',
        [
          
        ],
      ],
      Email: [ '',
        [
          Validators.required,
          Validators.email,
        ],
      ],
      Mobile: [ '',
      [
        Validators.required,
        Validators.pattern('^((\\?)|0)?[0-9]{10}$'),
      ],
    ],
      CreatePassword: ['',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(15),
        ],
      ],
      ConfirmPassword: ['',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(15),
        ],
      ],
      
    });
  }
  onSubmit(form: FormGroup) {
    if (form.invalid) {
      return;
    }
    let user = new User();
   
    user.UserName = form.value.UserName;
    user.FirstName=form.value.FirstName;
    user.LastName=form.value.LastName;
    user.Email = form.value.Email;
    user.CreatePassword = form.value.CreatePassword;
    user.ConfirmPassword=form.value.ConfirmPassword;
    user.Mobile = form.value.Mobile;
    console.log(user);
    this.userService.Register(user).subscribe(
      (result: any) => {
        console.warn(result);
        this.errMsg = 'Registered Successfully ';
        this.logService.log('User registered successfully!');
        this.router.navigate(['/login']); 
      },
      (err: any) => {
        console.log(err);
        this.logService.error("Registration failed");
        this.errMsg = 'Please Fill all the required Fields ';
      }
    );
    form.reset();
    window.alert('User Registered Successfully');

   
    
  }
}
