import { TestBed, } from '@angular/core/testing';
import { LogService } from './log.service';

describe('LogService', () => {
  let service: LogService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LogService],
    });

    service = TestBed.inject(LogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should log a message with "LOG" prefix', () => {
    spyOn(console, 'log');
    const message = 'Test Log Message';

    service.log(message);

    expect(console.log).toHaveBeenCalledWith('[LOG]: ' + message);
  });

  it('should log an error message with "ERROR" prefix', () => {
    spyOn(console, 'error');
    const message = 'Test Error Message';

    service.error(message);

    expect(console.error).toHaveBeenCalledWith('[ERROR]: ' + message);
  });

  it('should log a warning message with "WARN" prefix', () => {
    spyOn(console, 'error');
    const message = 'Test Warning Message';

    service.warn(message);

    expect(console.error).toHaveBeenCalledWith('[WARN]: ' + message);
  });

  it('should log an info message with "INFO" prefix', () => {
    spyOn(console, 'log');
    const message = 'Test Info Message';

    service.info(message);

    expect(console.log).toHaveBeenCalledWith('[INFO]: ' + message);
  });

  it('should use console.error for "ERROR" and "WARN" prefixes', () => {
    spyOn(console, 'error');
    spyOn(console, 'log');
    const errorMessage = 'Test Error Message';
    const warnMessage = 'Test Warning Message';

    service.error(errorMessage);
    service.warn(warnMessage);

    expect(console.error).toHaveBeenCalledWith('[ERROR]: ' + errorMessage);
    expect(console.error).toHaveBeenCalledWith('[WARN]: ' + warnMessage);
    expect(console.log).not.toHaveBeenCalled();
  });
});
