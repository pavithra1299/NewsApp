import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  log(message: string): void {
    this.logWithPrefix('LOG', message);
  }
  error(message: string): void {
    this.logWithPrefix('ERROR', message);
  }
  warn(message: string): void {
    this.logWithPrefix('WARN', message);
  }
  info(message: string): void {
    this.logWithPrefix('INFO', message);
  }
  private logWithPrefix(prefix: string, message: string): void {
    // Use console.error for both 'ERROR' and 'WARN' prefixes
    const logFunction = prefix === 'ERROR' || prefix === 'WARN' ? console.error : console.log;
    logFunction(`[${prefix}]: ${message}`);
  }
}

