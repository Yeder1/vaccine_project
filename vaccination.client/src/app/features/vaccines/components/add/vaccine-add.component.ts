import { Component } from '@angular/core';
import { VaccineDTO } from '../../models/vaccinceDTO';
import { VaccineService } from '../../services/vaccines.service';
import { VaccineTypeService } from '../../services/vacine-types.service';

import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { Router } from '@angular/router';
import { VaccineTypeDTO } from '../../../../models/vaccineType/vacineTypeDTO';

@Component({
  selector: 'app-vaccine-add',
  templateUrl: './vaccine-add.component.html',
  styleUrls: ['./vaccine-add.component.scss'],
})
export class VaccineAddComponent {
  vaccineForm: FormGroup;
  vaccineTypes: VaccineTypeDTO[] = [];

  constructor(
    private fb: FormBuilder,
    private vaccineService: VaccineService,
    private vaccineTypeService: VaccineTypeService,
    private router: Router
  ) {
    this.vaccineForm = this.fb.group(
      {
        vaccineName: ['', [Validators.required, Validators.maxLength(100)]],
        numberOfInjection: ['', Validators.required],
        usage: ['', Validators.maxLength(200)],
        indication: ['', Validators.maxLength(200)],
        contraindication: ['', Validators.maxLength(200)],
        origin: ['', Validators.maxLength(50)],
        nextBeginNextInjection: [''],
        nextEndNextInjection: [''],
        vaccineTypeId: ['', Validators.required],
      },
      {
        validators: this.compareDatesValidator(
          'nextBeginNextInjection',
          'nextEndNextInjection'
        ), 
      }
    );
  }

  ngOnInit(): void {
    // Fetch vaccine types on component initialization
    this.vaccineTypeService.getVaccineTypes().subscribe({
      next: (types) => {
        this.vaccineTypes = types;
        console.log(this.vaccineTypes);
      },
      error: (error) => console.error('Error fetching vaccine types', error),
    });
  }

  onSubmit(): void {
    const newVaccine: VaccineDTO = this.vaccineForm.value;
    console.log(newVaccine);
    this.vaccineService.addVaccine(newVaccine).subscribe({
      next: () => {
        this.router.navigate(['/vaccines']);
      },
      error: (error) => console.error('Error creating vaccines', error),
    });
  }

  onReset() {
    this.vaccineForm.reset();
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
