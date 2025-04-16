import { Component } from '@angular/core';

import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidationErrors,
  ValidatorFn
} from "@angular/forms"
import { from } from 'rxjs';
import { Router } from '@angular/router';
import { VaccineTypeDTO } from 'src/app/models/vaccineType/vacineTypeDTO';
import { VaccineTypeService } from '../../services/vaccine-type.service';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class VaccineTypeAddComponent {
  vaccineTypeForm: FormGroup;
  vaccineTypeTypes: VaccineTypeDTO[] = [];
  imageUrl: string | ArrayBuffer | null | undefined = null;
  imageFile: File | null = null;

  constructor(
    private fb: FormBuilder,
    private vaccineTypeService: VaccineTypeService,
    private router: Router,
    private faIconLibrary: FaIconLibrary,
  ) {
    faIconLibrary.addIconPacks(fas)

    this.vaccineTypeForm = this.fb.group(
      {
        vaccineTypeName: ['', [Validators.required, Validators.maxLength(100)]],
        vaccineTypeCode: ['', Validators.required],
        isActive: [false], // Ensure the default is set to false
        description: [''],
        image: [null]
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
    // Fetch vaccineType types on component initialization
    this.vaccineTypeService.getAllVaccineTypes().subscribe({
      next: (types) => {
        this.vaccineTypeTypes = types;
        console.log(this.vaccineTypeTypes);
      },
      error: (error) => console.error('Error fetching vaccine types', error),
    });
  }

  onSubmit(): void {
    if (this.vaccineTypeForm.valid) {
      const updatedVaccineType: VaccineTypeDTO = {
        ...this.vaccineTypeForm.getRawValue(),
        isDeleted: false
      };

      const request = new FormData();
      if (updatedVaccineType) {
        request.append('VaccineType', JSON.stringify(updatedVaccineType));
      }
      if (this.imageFile) {
        request.append('Image', this.imageFile); // Append the actual file to FormData
      }
      console.log(request);

      this.vaccineTypeService.addVaccineType(request).subscribe(
        () => {
          alert('Vaccine type updated successfully!');
          this.router.navigate(['/vaccine-type']); // Redirect to the vaccines list
        },
        (error: any) => {
          console.error('Error updating vaccine type', error);
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }

  onReset() {
    this.vaccineTypeForm.reset();

    // Clear image preview
    this.imageUrl = null;

    // Clear validation states
    this.vaccineTypeForm.markAsPristine();
    this.vaccineTypeForm.markAsUntouched();
    this.vaccineTypeForm.updateValueAndValidity();
  }

  onCancel() {
    this.router.navigate(['/vaccine-type']);
  }

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.imageFile = fileInput.files[0];

      const reader = new FileReader();
      reader.onload = (e) => {
        if (e.target?.result) {
          this.imageUrl = e.target.result; // Assign only if result is defined
        }
      };
      reader.readAsDataURL(this.imageFile);
    }
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
