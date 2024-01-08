import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NewspageComponent } from './newspage.component';
import { ExternaldataService } from '../externaldata.service';
import { WishserviceService } from '../wishservice.service';
import { RouterTestingModule } from '@angular/router/testing';
import { LogService } from '../log.service';
import { of } from 'rxjs';
import { Router } from '@angular/router';

describe('NewspageComponent', () => {
  let component: NewspageComponent;
  let fixture: ComponentFixture<NewspageComponent>;
  let mockExternalDataService: jasmine.SpyObj<ExternaldataService>;
  let mockWishlistService: jasmine.SpyObj<WishserviceService>;
  let mockLogService: jasmine.SpyObj<LogService>;

  beforeEach(waitForAsync(() => {
    mockExternalDataService = jasmine.createSpyObj('ExternaldataService', ['getNews']);
    mockWishlistService = jasmine.createSpyObj('WishserviceService', ['AddToWishlist']);
    mockLogService = jasmine.createSpyObj('LogService', ['log', 'info']);

    TestBed.configureTestingModule({
      declarations: [NewspageComponent],
      providers: [
        { provide: ExternaldataService, useValue: mockExternalDataService },
        { provide: WishserviceService, useValue: mockWishlistService },
        { provide: LogService, useValue: mockLogService },
        Router
      
      ],
      imports: [RouterTestingModule],
    }).compileComponents();
    fixture=TestBed.createComponent(NewspageComponent);
    component=fixture.componentInstance;  
  }));


  // beforeEach(() => {
  //   fixture = TestBed.createComponent(NewspageComponent);
  //   component = fixture.componentInstance;
  //   fixture.detectChanges();
  // });

  it('should create', () => {    
    expect(component).toBeTruthy();
  });

  it('should fetch news on ngOnInit', () => {
    const mockNews = [{ title: 'Test News' }];
    mockExternalDataService.getNews.and.returnValue(of(mockNews));

    component.ngOnInit();

    expect(component.news).toEqual(mockNews);
    expect(mockExternalDataService.getNews).toHaveBeenCalledWith('sports');
  });

  it('should fetch news on search', () => {
    const mockNews = [{ title: 'Test News' }];
    mockExternalDataService.getNews.and.returnValue(of(mockNews));

    component.onSearch();

    expect(component.news).toEqual(mockNews);
    expect(mockExternalDataService.getNews).toHaveBeenCalled();
  });

  it('should add news to wishlist', () => {
    const mockFavouriteNews = { title: 'Favorite News' };
    mockWishlistService.AddToWishlist.and.returnValue(of(mockFavouriteNews));

    component.addToFavorites(mockFavouriteNews);

    expect(component.news).toEqual(mockFavouriteNews);
    expect(mockWishlistService.AddToWishlist).toHaveBeenCalledWith(mockFavouriteNews, component.userName);
  });



  it('should log out and navigate to home page', () => {
    component.logout();

    expect(mockLogService.info).toHaveBeenCalledWith('User logged out');
    expect(localStorage.clear).toHaveBeenCalled();
    expect(sessionStorage.clear).toHaveBeenCalled();
    expect(component.router.navigate).toHaveBeenCalledWith(['']);
  });
});
