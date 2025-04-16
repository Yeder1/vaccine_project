import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ErrorResponseValidationAttributes } from './models/errors';

@Injectable({
  providedIn: 'root', // Makes it available application-wide
})

export class ErrorHandlerService {
  constructor(private messageService: MessageService) { }
  /**
   * Handles and logs errors, and displays error messages using the message service.
   *
   * @param error - The error object containing error details.
   */
  handleError(error: ErrorResponseValidationAttributes): void {
    console.error(error.error.errors);

    if (error.error.errors) {
      const errorMessages = Object.entries(error.error.errors)
        .map(([key, value]) => `${key}: ${value}`)
        .join(', ');
      console.error(errorMessages);
      this.messageService.add({ severity: 'error', summary: 'Error', detail: errorMessages });
    }
  }
}
