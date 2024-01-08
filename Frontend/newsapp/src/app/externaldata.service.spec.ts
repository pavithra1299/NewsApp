import { TestBed, inject } from '@angular/core/testing';
import { ExternaldataService } from './externaldata.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('ExternaldataService', () => {
  let service: ExternaldataService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ExternaldataService]
    });

    service = TestBed.inject(ExternaldataService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve news data from the API via GET', () => {
    const searchQuery = 'sports';
    const mockNewsData = [{ title: 'News 1' }, { title: 'News 2' }];

    service.getNews(searchQuery).subscribe(data => {
      expect(data).toEqual(mockNewsData);
    });

    const req = httpMock.expectOne(`https://localhost:7189/api/News/${searchQuery}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockNewsData);
  });

  it('should handle error during news fetching', () => {
    const searchQuery = 'sports';

    service.getNews(searchQuery).subscribe(
      () => fail('should have failed with the 404 error'),
      (error) => {
        expect(error.status).toBe(404);
      }
    );

    const req = httpMock.expectOne(`https://localhost:7189/api/News/${searchQuery}`);
    expect(req.request.method).toBe('GET');
    req.flush('Not Found', { status: 404, statusText: 'Not Found' });
  });
});
