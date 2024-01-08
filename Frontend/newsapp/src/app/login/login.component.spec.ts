import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';
import { LogService } from '../log.service';
import { of, throwError } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let mockLoginService: jasmine.SpyObj<LoginService>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockLogService: jasmine.SpyObj<LogService>;
  

  beforeEach(waitForAsync(() => {
    mockLoginService = jasmine.createSpyObj('LoginService', ['Login']);
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    mockLogService = jasmine.createSpyObj('LogService', ['log', 'error']);

    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      providers: [
        { provide: LoginService, useValue: mockLoginService },
        { provide: Router, useValue: mockRouter },
        { provide: LogService, useValue: mockLogService },
        
      ],
      imports: [ReactiveFormsModule],
    }).compileComponents();
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  // beforeEach(() => {
  //   fixture = TestBed.createComponent(LoginComponent);
  //   component = fixture.componentInstance;
  //   fixture.detectChanges();
  // });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with proper controls', () => {
    const formControls = component.LoginForm.controls;

    expect(formControls.UserName).toBeDefined();
    expect(formControls.Password).toBeDefined();
  });

  it('should require both fields for form submission', () => {
    const form = component.LoginForm;
    form.setValue({
      UserName: '',
      Password: '',
    });

    expect(form.invalid).toBe(true);
    expect(component.onSubmit(form)).toBeUndefined();
  });

  it('should call Login method on form submission', () => {
    const form = component.LoginForm;
    form.setValue({
      UserName: 'testuser',
      Password: 'password123',
    });

    mockLoginService.Login.and.returnValue(of({ token: 'fakeToken' }));
    component.onSubmit(form);

    expect(mockLoginService.Login).toHaveBeenCalled();
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/home']);
    expect(mockLogService.log).toHaveBeenCalledWith('Login success!!');
    expect(sessionStorage.getItem('token')).toEqual('fakeToken');
    expect(localStorage.getItem('userName')).toEqual('testuser');
  });

  it('should handle login error', () => {
    const form = component.LoginForm;
    form.setValue({
      UserName: 'testuser',
      Password: 'password123',
    });

    const errorResponse = { status: 401, error: 'Unauthorized' };
    mockLoginService.Login.and.returnValue(throwError(errorResponse));
    component.onSubmit(form);

    expect(mockLoginService.Login).toHaveBeenCalled();
    expect(mockRouter.navigate).not.toHaveBeenCalled();
    expect(mockLogService.error).toHaveBeenCalledWith('Login failed..');
    expect(component.isLoggedin).toBe(false);
    expect(component.errMsg).toEqual('Please Fill all the required Fields ');
  });
});
