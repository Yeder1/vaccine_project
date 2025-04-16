import { Component } from '@angular/core';
import { VaccineDTO } from '../../models/vaccinceDTO';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VaccineService } from '../../services/vaccines.service';
import { VaccineTypeDTO } from '../../../../models/vaccineType/vacineTypeDTO';
import { VaccineTypeService } from '../../services/vacine-types.service';

@Component({
  selector: 'app-vaccine-update',
  templateUrl: './vaccine-update.component.html',
  styleUrls: ['./vaccine-update.component.scss'],
})
export class VaccineUpdateComponent {
  vaccineId!: number;
  vaccineForm: FormGroup;
  vaccineTypes: VaccineTypeDTO[] = [];
  vaccineData!: VaccineDTO;
  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private vaccineService: VaccineService,
    private vaccineTypeService: VaccineTypeService,
    private fb: FormBuilder
  ) {
    this.vaccineForm = this.fb.group(
      {
        id: [],
        vaccineName: ['', [Validators.required, Validators.maxLength(100)]],
        numberOfInjection: ['', Validators.required],
        usage: ['', Validators.maxLength(200)],
        indication: ['', Validators.maxLength(200)],
        contraindication: ['', Validators.maxLength(200)],
        origin: ['', Validators.maxLength(50)],
        nextBeginNextInjection: [''],
        nextEndNextInjection: [''],
        vaccineTypeId: ['', Validators.required],
        status: [true],
      },
      {
        validators: this.compareDatesValidator(
          'nextBeginNextInjection',
          'nextEndNextInjection'
        ),
      }
    );
  }

  formatDate(dateTime: Date | string | undefined): string {
    if (!dateTime) {
      return ''; // Return an empty string if dateTime is undefined or null
    }

    // If the value is a Date object, convert it to a string in "yyyy-MM-dd" format
    if (dateTime instanceof Date) {
      const year = dateTime.getFullYear();
      const month = ('0' + (dateTime.getMonth() + 1)).slice(-2); // Add leading zero if needed
      const day = ('0' + dateTime.getDate()).slice(-2); // Add leading zero if needed
      return `${year}-${month}-${day}`;
    }

    // If the value is a string, assume it's in ISO format and return the date part
    return dateTime.split('T')[0];
  }
  ngOnInit(): void {
    this.vaccineId = Number(this.route.snapshot.paramMap.get('id'));
    this.vaccineTypeService.getVaccineTypes().subscribe({
      next: (types) => {
        this.vaccineTypes = types;
      },
      error: (error) => console.error('Error fetching vaccine types', error),
    });
    this.loadVaccine();
  }

  loadVaccine(): void {
    this.vaccineService.getVaccineById(this.vaccineId).subscribe({
      next: (vaccine) => {
        this.vaccineData = vaccine;
        this.vaccineForm.patchValue({
          ...vaccine,
          nextBeginNextInjection: this.formatDate(
            vaccine.nextBeginNextInjection
          ),
          nextEndNextInjection: this.formatDate(vaccine.nextEndNextInjection),
          status: vaccine.status,
        });
      },
      error: (error) => console.error('Error fetching book', error),
    });
  }

  onSubmit(): void {
    if (this.vaccineForm.valid) {
      const updatedVaccine: VaccineDTO = {
        id: this.vaccineId,
        ...this.vaccineForm.value,
      };
      this.vaccineService.updateVaccine(updatedVaccine).subscribe(
        () => {
          alert('Vaccine updated successfully!');
          this.router.navigate(['/vaccines']); // Redirect to the vaccines list
        },
        (error: any) => {
          console.error('Error updating vaccine:', error);
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }

  onReset() {
    this.vaccineForm.patchValue({
      ...this.vaccineData,
      nextBeginNextInjection: this.formatDate(
        this.vaccineData.nextBeginNextInjection
      ),
      nextEndNextInjection: this.formatDate(
        this.vaccineData.nextEndNextInjection
      ),
      status: this.vaccineData.status,
    });
  }

  onCancel() {
    this.router.navigate(['/vaccines']);
  }

  compareDatesValidator(startField: string, endField: string): ValidatorFn {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const startDate = formGroup.get(startField)?.value;
      const endDate = formGroup.get(endField)?.value;

      if (startDate && endDate && new Date(endDate) <= new Date(startDate)) {
        formGroup.get(endField)?.setErrors({ dateMismatch: true });
      } else {
        formGroup.get(endField)?.setErrors(null);
      }
      return null;
    };
  }
}
