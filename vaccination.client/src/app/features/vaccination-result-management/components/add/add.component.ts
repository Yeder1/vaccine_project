import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  ConfirmationService,
  MessageService,
  ConfirmEventType,
} from 'primeng/api';
import { concatMap, map, Observable, Subscription } from 'rxjs';
import { VaccinationResultService } from '../../vaccination-result-management.service';
import { Router } from '@angular/router';
import { VaccineTypeDTO } from 'src/app/models/vaccinationResult/vaccineTypeDTO';
import { InjectionResultCustomerDTO } from 'src/app/models/vaccinationResult/vaccinationResultDTO';
import { InjectionResultCustomersDTOResponse, VaccinationResultListResponse, VaccineTypeDTOsResponse } from 'src/app/models/vaccinationResult/vaccinationResultResponse';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  vaccineForm!: FormGroup;
  subscriptions: Subscription[] = [];
  vaccineType$: Observable<VaccineTypeDTO[]> | undefined;
  customers$: Observable<InjectionResultCustomerDTO[]> | undefined;

  constructor(private fb: FormBuilder,
    private vaccinationResultService: VaccinationResultService,
    private messageService: MessageService,
    private errorHandler: ErrorHandlerService,
    private router: Router) { }

  ngOnInit() {
    this.vaccineForm = this.fb.group({
      customerId: ['', Validators.required],
      prevention: ['', Validators.required],
      vaccineId: ['', Validators.required],
      numberOfInjection: ['', Validators.required],
      injectionDate: ['', Validators.required],
      nextInjectionDate: ['', Validators.required],
      injectionPlace: ['', Validators.required]
    });

    this.vaccineType$ = this.vaccinationResultService.getAllVaccineType().pipe(
      map((response: VaccineTypeDTOsResponse) => response.data)
    );

    this.customers$ = this.vaccineType$.pipe(
      concatMap(() => this.vaccinationResultService.getAllCustomer().pipe(
        map((response: InjectionResultCustomersDTOResponse) => response.data)
      ))
    );

  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  onSubmit() {

    const injectionDate = this.vaccineForm.get('injectionDate')?.value;
    const nextInjectionDate = this.vaccineForm.get('nextInjectionDate')?.value;

    // if (injectionDate && nextInjectionDate && new Date(injectionDate) > new Date(nextInjectionDate)) {
    //   this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Injection date must be less than next injection date' });
    //   return;
    // }


    if (this.vaccineForm.valid) {
      console.log(this.vaccineForm.value);
      this.vaccinationResultService.createVaccinationResult(this.vaccineForm.value).subscribe({
        next: (data) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Vaccination result is created' });
          setTimeout(() => {
            this.router.navigate(['/vaccination-result-management']);
          }, 1000);
        },
        error: (error) => {
          this.errorHandler.handleError(error);
        }
      })
      // Handle form submission
    } else {
      // Mark all fields as touched to trigger validation messages
      Object.keys(this.vaccineForm.controls).forEach(key => {
        const control = this.vaccineForm.get(key);
        if (control) {
          control.markAsTouched();
        }
      });
    }
  }

  onReset() {
    this.vaccineForm.reset();
  }

  onCancel() {
    // Handle cancel action (e.g., navigate away)
    this.router.navigate(['/vaccination-result-management']);
  }
}
