import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { UserServiceService } from './user-service.service';
import { User } from './user';

describe('UserServiceService', () => {
  let service: UserServiceService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UserServiceService, { provide: Router, useClass: class { navigate = jasmine.createSpy('navigate'); } }]
    });

    service = TestBed.inject(UserServiceService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send registration request and return result', () => {
    const mockUser: User = {
      UserName: 'testUser',
      FirstName: 'John',
      LastName: 'Doe',
      Email: 'john.doe@example.com',
      Mobile: '1234567890',
      CreatePassword: 'password123',
      ConfirmPassword: 'password123',
    };

    service.Register(mockUser).subscribe((result: any) => {
      expect(result).toBeDefined();
    });

    const req = httpMock.expectOne('https://localhost:7172/api/user');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockUser);

    req.flush({/* mock response data */});
  });

  it('should navigate to login after successful registration', () => {
    const mockUser: User = {
      UserName: 'testUser',
      FirstName: 'John',
      LastName: 'Doe',
      Email: 'john.doe@example.com',
      Mobile: '1234567890',
      CreatePassword: 'password123',
      ConfirmPassword: 'password123',
    };

    service.Register(mockUser).subscribe(() => {
      expect(TestBed.inject(Router).navigate).toHaveBeenCalledWith(['/login']);
      
    });

    const req = httpMock.expectOne('https://localhost:7172/api/user');
    req.flush({/* mock response data */});
  });
});
