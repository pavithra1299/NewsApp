import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { WishserviceService } from './wishservice.service';

describe('WishserviceService', () => {
  let service: WishserviceService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [WishserviceService],
    });

    service = TestBed.inject(WishserviceService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send addToWishlist request and return result', () => {
    const mockFavouriteNews = { /* mock FavouriteNews data */ };
    const mockUserName = 'testUser';

    service.AddToWishlist(mockFavouriteNews, mockUserName).subscribe(result => {
      expect(result).toBeDefined();
    });

    const req = httpMock.expectOne(`https://localhost:7179/api/Wishlist?userName=${mockUserName}`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockFavouriteNews);

    req.flush({/* mock response data */});
  });

  it('should send getWishlist request and return result', () => {
    const mockUserName = 'testUser';

    service.GetWishlist(mockUserName).subscribe(result => {
      expect(result).toBeDefined();
    });

    const req = httpMock.expectOne(`https://localhost:7179/api/Wishlist?userName=${mockUserName}`);
    expect(req.request.method).toBe('GET');

    req.flush({/* mock response data */});
  });

  it('should send deleteFromWishlist request and return result', () => {
    const mockUserName = 'testUser';
    const mockAuthor = 'testAuthor';

    service.DeleteFromWishlist(mockUserName, mockAuthor).subscribe(result => {
      expect(result).toBeDefined();
    });

    const req = httpMock.expectOne(`https://localhost:7179/api/Wishlist/${mockAuthor}?userName=${mockUserName}`);
    expect(req.request.method).toBe('DELETE');

    req.flush({/* mock response data */});
  });
});
