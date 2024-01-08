import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HomepageComponent } from './homepage.component';
import { ExternaldataService } from '../externaldata.service';
import { LogService } from '../log.service';
import { of } from 'rxjs';

describe('HomepageComponent', () => {
  let component: HomepageComponent;
  let fixture: ComponentFixture<HomepageComponent>;
  let externalDataService: jasmine.SpyObj<ExternaldataService>;
  let logService: jasmine.SpyObj<LogService>;

  beforeEach(() => {
    const externalDataServiceSpy = jasmine.createSpyObj('ExternaldataService', ['getNews']);
    const logServiceSpy = jasmine.createSpyObj('LogService', ['log']);

    TestBed.configureTestingModule({
      declarations: [HomepageComponent],
      providers: [
        { provide: ExternaldataService, useValue: externalDataServiceSpy },
        { provide: LogService, useValue: logServiceSpy },
      ],
    });

    fixture = TestBed.createComponent(HomepageComponent);
    component = fixture.componentInstance;
    externalDataService = TestBed.inject(ExternaldataService) as jasmine.SpyObj<ExternaldataService>;
    logService = TestBed.inject(LogService) as jasmine.SpyObj<LogService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch news on initialization', () => {
    const mockNews = [{ title: 'News 1' }, { title: 'News 2' }];
    externalDataService.getNews.and.returnValue(of(mockNews));

    component.ngOnInit();

    expect(externalDataService.getNews).toHaveBeenCalledWith('sports');
    expect(component.news).toEqual(mockNews);
    expect(logService.log).toHaveBeenCalledWith('Data is fetched from External API');
  });


});
