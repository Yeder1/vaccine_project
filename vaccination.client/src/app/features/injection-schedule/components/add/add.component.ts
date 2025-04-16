import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InjectionScheduleService } from '../../services/injection-schedule.service';
import { InjectionSchedule, InjectionScheduleStatus } from '../../models/injection-schedule.model';
import { VaccineService } from '../../../vaccines/services/vaccines.service';
import { VaccineDTO } from 'src/app/features/vaccines/models/vaccinceDTO';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  addForm: FormGroup;
  dateError: string = '';
  vaccines: VaccineDTO[] = [];
  statusOptions = Object.values(InjectionScheduleStatus);

  constructor(
    private fb: FormBuilder,
    private injectionScheduleService: InjectionScheduleService,
    private vaccineService: VaccineService,
    private router: Router
  ) {
    this.addForm = this.fb.group({
      description: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      place: ['', Validators.required],
      vaccineId: ['', Validators.required],
      status: ['', Validators.required]
    });

    // Add valueChanges listener for date fields
    this.addForm.get('startDate')?.valueChanges.subscribe(() => this.validateDates());
    this.addForm.get('endDate')?.valueChanges.subscribe(() => this.validateDates());
  }

  ngOnInit() {
    this.vaccineService.getAllVaccines().subscribe({
      next: (vaccines) => {
        this.vaccines = vaccines;
      },
      error: (error) => {
        console.error('Error fetching vaccines', error);
      }
    });

    // You can initialize any data here if needed
  }

  validateDates() {
    const startDate = this.addForm.get('startDate')?.value;
    const endDate = this.addForm.get('endDate')?.value;
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0); // Set time to midnight for accurate comparison

    if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
      this.dateError = 'From date must be less than to date';
      this.addForm.setErrors({ invalidDateRange: true });
    } else {
      this.dateError = '';
      this.addForm.setErrors(null);
    }
  }

  onSubmit() {
    if (this.addForm.valid && !this.dateError) {
      const newSchedule: InjectionSchedule = this.addForm.value;
      this.injectionScheduleService.create(newSchedule).subscribe({
        next: (response) => {
          console.log('Injection schedule created successfully', response);
          this.router.navigate(['/injection-schedule/list']);
        },
        error: (error) => {
          console.error('Error creating injection schedule', error);
          // Handle error (e.g., show error message to user)
        }
      });
    }
  }

  onCancel() {
    this.router.navigate(['/injection-schedule/list']);
  }
}
