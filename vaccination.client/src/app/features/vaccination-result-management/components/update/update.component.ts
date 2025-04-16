
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VaccinationResultService } from '../../vaccination-result-management.service';
import { InjectionResultCustomerDTO, VaccinationResultDTO } from 'src/app/models/vaccinationResult/vaccinationResultDTO';
import { MessageService } from 'primeng/api';
import { Subscription, Observable, map, concatMap, shareReplay } from 'rxjs';
import { VaccineTypeDTOsResponse, InjectionResultCustomersDTOResponse, VaccinationResultDTOResponse } from 'src/app/models/vaccinationResult/vaccinationResultResponse';
import { VaccineTypeDTO } from 'src/app/models/vaccinationResult/vaccineTypeDTO';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent {
  vaccineForm!: FormGroup;
  subscriptions: Subscription[] = [];
  vaccineType$: Observable<VaccineTypeDTO[]> | undefined;
  customers$: Observable<InjectionResultCustomerDTO[]> | undefined;

  vaccinationResultDTO$: Observable<VaccinationResultDTO> | undefined;

  constructor(private fb: FormBuilder,
    private vaccinationResultService: VaccinationResultService,
    private messageService: MessageService,
    private errorHandler: ErrorHandlerService,
    private router: Router,
    private datePipe: DatePipe,
    private activeRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.vaccineForm = this.fb.group({
      id: ['', Validators.required],
      customerId: ['', Validators.required],
      prevention: ['', Validators.required],
      vaccineId: ['', Validators.required],
      numberOfInjection: ['', Validators.required],
      injectionDate: ['', Validators.required],
      nextInjectionDate: ['', Validators.required],
      injectionPlace: ['', Validators.required]
    });

    // this.activeRoute.paramMap.subscribe({
    //   next: (params) => {
    //     if (params.get('id')) {
    //       console.log(params.get('id'));
    //       this.vaccinationResultDTO$ = this.vaccinationResultService.getVaccinationResultById(params.get('id')!).pipe(
    //         map((response: VaccinationResultDTOResponse) => response.data),
    //         map((data: VaccinationResultDTO) => {
    //           console.log(data);
    //           this.vaccineForm.patchValue({
    //             id: data.id,
    //             customerId: data.customer.id,
    //             prevention: data.prevention,
    //             vaccineId: data.vaccine.id,
    //             numberOfInjection: data.numberOfInjection,
    //             injectionDate: this.datePipe.transform(data.injectionDate, 'yyyy-MM-dd'),
    //             nextInjectionDate: this.datePipe.transform(data.nextInjectionDate, 'yyyy-MM-dd'),
    //             injectionPlace: data.injectionPlace
    //           });
    //           return data;
    //         }),
    //       );
    //     }
    //   }
    // })


    this.activeRoute.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        console.log(id);
        this.vaccinationResultDTO$ = this.vaccinationResultService.getVaccinationResultById(id).pipe(
          map((response: VaccinationResultDTOResponse) => response.data),
          map((data: VaccinationResultDTO) => {
            this.vaccineForm.patchValue({
              id: data.id,
              customerId: data.customer.id,
              prevention: data.prevention,
              vaccineId: data.vaccine.id,
              numberOfInjection: data.numberOfInjection,
              injectionDate: this.datePipe.transform(data.injectionDate, 'yyyy-MM-dd'),
              nextInjectionDate: this.datePipe.transform(data.nextInjectionDate, 'yyyy-MM-dd'),
              injectionPlace: data.injectionPlace
            });
            return data;
          }),
        );
      }
    });


    this.vaccineType$ = this.vaccinationResultService.getAllVaccineType().pipe(
      map((response: VaccineTypeDTOsResponse) => response.data),
      // shareReplay(1)  // Cache the latest emission and share it with all subscribers
    );


    this.customers$ = this.vaccineType$.pipe(
      concatMap(() => this.vaccinationResultService.getAllCustomer().pipe(
        map((response: InjectionResultCustomersDTOResponse) => response.data),
        //shareReplay(1)  // Cache the latest emission and share it with all subscribers
      ))
    );


  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  onSubmit() {

    const injectionDate = this.vaccineForm.get('injectionDate')?.value;
    const nextInjectionDate = this.vaccineForm.get('nextInjectionDate')?.value;

    if (injectionDate && nextInjectionDate && new Date(injectionDate) > new Date(nextInjectionDate)) {
      this.messageService.add({ severity: 'error', summary: 'Validation Error', detail: 'Injection date must be less than next injection date' });
      return;
    }


    if (this.vaccineForm.valid) {
      console.log(this.vaccineForm.value);
      this.vaccinationResultService.updateVaccinationResult(this.vaccineForm.value).subscribe({
        next: (data) => {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Vaccination result is updated' });
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
