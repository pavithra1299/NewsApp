import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

describe('LoginService', () => {
  let service: LoginService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [LoginService, { provide: Router, useClass: class { navigate = jasmine.createSpy('navigate'); } }]
    });

    service = TestBed.inject(LoginService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send login request and return result', () => {
    const userId = 'testUser';
    const password = 'testPassword';
    const mockToken = 'testToken';

    service.Login(userId, password).subscribe((result: { token: any; }) => {
      expect(result.token).toEqual(mockToken);
    });

    const req = httpMock.expectOne('https://localhost:7209/api/Auth/login');
    expect(req.request.method).toBe('POST');
    expect(req.request.body.UserId).toBe(userId);
    expect(req.request.body.Password).toBe(password);

    req.flush({ token: mockToken });
  });

  it('should check if user is logged in', () => {
    // Assuming that if there is a result, the user is logged in
    service.result = { token: 'testToken' };
    expect(service.isLoggedIn()).toBe(true);

    // Assuming that if there is no result, the user is not logged in
    service.result = null;
    expect(service.isLoggedIn()).toBe(false);
  });

  it('should clear localStorage on logout if user is logged in', () => {
    // Assuming that if there is a result, the user is logged in
    service.result = { token: 'testToken' };
    spyOn(localStorage, 'clear');

    service.logout();

    expect(localStorage.clear).toHaveBeenCalled();
  });

  it('should not clear localStorage on logout if user is not logged in', () => {
    // Assuming that if there is no result, the user is not logged in
    service.result = null;
    spyOn(localStorage, 'clear');

    service.logout();

    expect(localStorage.clear).not.toHaveBeenCalled();
  });
});
