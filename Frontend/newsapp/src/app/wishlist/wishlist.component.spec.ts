import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { WishlistComponent } from './wishlist.component';
import { WishserviceService } from '../wishservice.service';
import { Router } from '@angular/router';
import { LogService } from '../log.service';
import { Observable, of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';

describe('WishlistComponent', () => {
  let component: WishlistComponent;
  let fixture: ComponentFixture<WishlistComponent>;
  let mockWishService: jasmine.SpyObj<WishserviceService>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockLogService: jasmine.SpyObj<LogService>;
  
  

  beforeEach(waitForAsync(() => {
    mockWishService = jasmine.createSpyObj('WishserviceService', ['GetWishlist', 'DeleteFromWishlist']);
    
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    mockLogService = jasmine.createSpyObj('LogService', ['info', 'error']);

    TestBed.configureTestingModule({
      declarations: [WishlistComponent],      
      providers: [
        { provide: WishserviceService, useValue: mockWishService },
        { provide: Router, useValue: mockRouter },
        { provide: LogService, useValue: mockLogService },
        Router,
        
      ],
      imports:[RouterTestingModule]
    }).compileComponents();
    fixture=TestBed.createComponent(WishlistComponent);
    component=fixture.componentInstance;
    
  }));

  // beforeEach(() => {
  //   fixture = TestBed.createComponent(WishlistComponent);
  //   component = fixture.componentInstance;
  //   fixture.detectChanges();
  // });

  it('should create', () => {     
    expect(component).toBeTruthy();
  });

  it('should fetch wishlist on ngOnInit', () => {
    const mockWishlist = [{ title: 'Test News', author: 'John Doe' }];
    
    mockWishService.GetWishlist.and.returnValue(of(mockWishlist));
    fixture.detectChanges();
    component.ngOnInit();
    

    expect(component.news).toEqual(mockWishlist);
    expect(mockWishService.GetWishlist).toHaveBeenCalledWith(component.userName);
    expect(mockLogService.info).toHaveBeenCalledWith('Fetched user wishlist');
  });

  it('should delete news from wishlist', () => {
    const mockNewsAuthor = 'John Doe';
    mockWishService.DeleteFromWishlist.and.returnValue(of([]));

    component.delete(mockNewsAuthor);

    expect(mockWishService.DeleteFromWishlist).toHaveBeenCalledWith(component.userName, mockNewsAuthor);
    expect(window.alert).toHaveBeenCalledWith('News deleted');
    expect(mockLogService.info).toHaveBeenCalledWith('News deleted from wishlist');
    expect(mockRouter.navigate).toHaveBeenCalledWith(['./wishlist']);
  });

  it('should handle delete error', () => {
    const mockNewsAuthor = 'John Doe';
    const errorResponse = { status: 500, error: 'Internal Server Error' };
    mockWishService.DeleteFromWishlist.and.returnValue(of([]));

    component.delete(mockNewsAuthor);

    expect(mockWishService.DeleteFromWishlist).toHaveBeenCalledWith(component.userName, mockNewsAuthor);
    expect(window.alert).not.toHaveBeenCalled();
    expect(mockLogService.error).toHaveBeenCalledWith('News not deleted');
    expect(console.error).toHaveBeenCalledWith('Delete failed', errorResponse);
  });

  it('should log out and navigate to home page', () => {
    component.logout();

    expect(mockLogService.info).toHaveBeenCalledWith('User logged Out');
    expect(localStorage.clear).toHaveBeenCalled();
    expect(sessionStorage.clear).toHaveBeenCalled();
    expect(mockRouter.navigate).toHaveBeenCalledWith(['']);
  });
});
