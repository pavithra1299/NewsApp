import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationComponent } from './registration.component';
import { UserServiceService } from '../user-service.service';
import { RouterTestingModule } from '@angular/router/testing';
import { LogService } from '../log.service';
import { of, throwError } from 'rxjs';
import { LoginComponent } from '../login/login.component';
import { Router } from '@angular/router';

describe('RegistrationComponent', () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;
  let mockUserService: jasmine.SpyObj<UserServiceService>;
  let mockLogService: jasmine.SpyObj<LogService>;
  

  beforeEach(waitForAsync(() => {
    mockUserService = jasmine.createSpyObj('UserServiceService', ['Register']);
    mockLogService = jasmine.createSpyObj('LogService', ['log', 'error']);

    TestBed.configureTestingModule({
      declarations: [RegistrationComponent],      
      providers: [
        { provide: UserServiceService, useValue: mockUserService },
        { provide: LogService, useValue: mockLogService },
        Router
      ],
      
      imports: [ReactiveFormsModule, RouterTestingModule],
    }).compileComponents();
    fixture=TestBed.createComponent(RegistrationComponent);
    component=fixture.componentInstance;
    fixture.detectChanges();
  }));

  // beforeEach(() => {
  //   fixture = TestBed.createComponent(RegistrationComponent);
  //   component = fixture.componentInstance;
  //   fixture.detectChanges();
  // });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with proper controls', () => {
    const formControls = component.myForm.controls;

    expect(formControls.UserName).toBeDefined();
    expect(formControls.FirstName).toBeDefined();
    expect(formControls.LastName).toBeDefined();
    expect(formControls.Email).toBeDefined();
    expect(formControls.Mobile).toBeDefined();
    expect(formControls.CreatePassword).toBeDefined();
    expect(formControls.ConfirmPassword).toBeDefined();
  });

  it('should require all fields for form submission', () => {
    const form = component.myForm;
    form.setValue({
      UserName: '',
      FirstName: '',
      LastName: '',
      Email: '',
      Mobile: '',
      CreatePassword: '',
      ConfirmPassword: '',
    });

    expect(form.invalid).toBe(true);
    expect(component.onSubmit(form)).toBeUndefined();
  });

  it('should call Register method on form submission', () => {
    const form = component.myForm;
    form.setValue({
      UserName: 'testuser',
      FirstName: 'John',
      LastName: 'Doe',
      Email: 'test@example.com',
      Mobile: '1234567890',
      CreatePassword: 'password123',
      ConfirmPassword: 'password123',
    });

    mockUserService.Register.and.returnValue(of({}));
    component.onSubmit(form);

    expect(mockUserService.Register).toHaveBeenCalled();
    expect(mockLogService.log).toHaveBeenCalledWith('User registered successfully!');
  });

  it('should handle registration error', () => {
    const form = component.myForm;
    form.setValue({
      UserName: 'testuser',
      FirstName: 'John',
      LastName: 'Doe',
      Email: 'test@example.com',
      Mobile: '1234567890',
      CreatePassword: 'password123',
      ConfirmPassword: 'password123',
    });

    const errorResponse = { status: 400, error: 'Bad Request' };
    mockUserService.Register.and.returnValue(throwError(errorResponse));
    component.onSubmit(form);

    expect(mockUserService.Register).toHaveBeenCalled();
    expect(mockLogService.error).toHaveBeenCalledWith('Registration failed');
    expect(component.errMsg).toEqual('Please Fill all the required Fields ');
  });
});
