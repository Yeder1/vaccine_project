import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InjectionScheduleService } from '../../services/injection-schedule.service';
import { InjectionSchedule } from '../../models/injection-schedule.model';
import { VaccineService } from '../../../vaccines/services/vaccines.service';
import { VaccineDTO } from '../../../vaccines/models/vaccinceDTO';
import { InjectionScheduleStatus } from '../../models/injection-schedule.model';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  updateForm: FormGroup;
  scheduleId: number = 0;
  dateError: string = '';
  vaccines: VaccineDTO[] = [];
  statusOptions = Object.values(InjectionScheduleStatus);

  constructor(
    private fb: FormBuilder,
    private injectionScheduleService: InjectionScheduleService,
    private router: Router,
    private route: ActivatedRoute,
    private vaccineService: VaccineService
  ) {
    this.updateForm = this.fb.group({
      description: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      place: ['', Validators.required],
      vaccineId: ['', Validators.required],
      status: ['', Validators.required]
    });

    this.updateForm.get('startDate')?.valueChanges.subscribe(() => this.validateDates());
    this.updateForm.get('endDate')?.valueChanges.subscribe(() => this.validateDates());
  }

  ngOnInit() {
    this.loadVaccines();
    const id = this.route.snapshot.paramMap.get('id');
    if (id !== null) {
      this.scheduleId = +id;
      this.loadScheduleData();
    } else {
      // Handle the case where no ID is provided, e.g., redirect to list page
      this.router.navigate(['/injection-schedule/list']);
    }
  }

  loadVaccines() {
    this.vaccineService.getAllVaccines().subscribe({
      next: (vaccines) => {
        this.vaccines = vaccines;
      },
      error: (error) => {
        console.error('Error fetching vaccines', error);
      }
    });
  }

  loadScheduleData() {
    this.injectionScheduleService.getById(this.scheduleId).subscribe({
      next: (schedule) => {
        this.updateForm.patchValue({
          description: schedule.description,
          startDate: schedule.startDate ? this.formatDateForInput(new Date(schedule.startDate)) : '',
          endDate: schedule.endDate ? this.formatDateForInput(new Date(schedule.endDate)) : '',
          place: schedule.place,
          vaccineId: schedule.vaccineId,
          status: schedule.status
        });
      },
      error: (error) => {
        console.error('Error loading schedule data', error);
      }
    });
  }

  formatDateForInput(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    return d.toLocaleDateString('en-CA'); // Returns YYYY-MM-DD in local time
  }

  validateDates() {
    const startDate = this.updateForm.get('startDate')?.value;
    const endDate = this.updateForm.get('endDate')?.value;
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    const start = startDate ? new Date(startDate) : null;
    const end = endDate ? new Date(endDate) : null;


    if (start && end && start > end) {
      this.dateError = 'Start date must be less than to date';
      this.updateForm.setErrors({ invalidDateRange: true });
    }
    else {
      this.dateError = '';
      this.updateForm.setErrors(null); // Xóa lỗi nếu tất cả hợp lệ
    }
  }


  onSubmit() {
    if (this.updateForm.valid && !this.dateError) {
      const updatedSchedule: InjectionSchedule = {
        id: this.scheduleId,
        ...this.updateForm.value,
        startDate: new Date(this.updateForm.value.startDate).toISOString(),
        endDate: new Date(this.updateForm.value.endDate).toISOString(),
      };

      console.log('Updating schedule with:', updatedSchedule);

      this.injectionScheduleService.update(updatedSchedule).subscribe({
        next: (response) => {
          console.log('Injection schedule updated successfully', response);
          this.router.navigate(['/injection-schedule/list']);
        },
        error: (error) => {
          console.error('Error updating injection schedule', error);
          if (error.error && error.error.message) {
            alert(`Error: ${error.error.message}`);
          } else {
            alert('An error occurred while updating the injection schedule. Please try again.');
          }
        }
      });
    } else {
      alert('Please fill in all required fields correctly before submitting.');
    }
  }

  onCancel() {
    this.router.navigate(['/injection-schedule/list']);
  }
}
