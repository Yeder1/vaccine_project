import { Component, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { VaccineTypeDTO } from 'src/app/models/vaccineType/vacineTypeDTO';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { VaccineTypeService } from '../../services/vaccine-type.service';
import { VaccineTypeRequestDTO } from 'src/app/models/vaccineType/vacineTypeRequestDTO';
import { environment } from 'src/environments/environment';
import { ImageReference } from 'src/environments/image-reference';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class VaccineTypeUpdateComponent {
  vaccineTypeId!: number;
  vaccineTypeForm: FormGroup;
  vaccineTypeTypes: VaccineTypeDTO[] = [];
  imageFile: File | null = null;
  imageUrl: string | ArrayBuffer | null | undefined = null;
  vaccineTypeData!: VaccineTypeDTO;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private vaccineTypeService: VaccineTypeService,
    private router: Router,
    private faIconLibrary: FaIconLibrary,
  ) {
    faIconLibrary.addIconPacks(fas)

    this.vaccineTypeForm = this.fb.group(
      {
        id: [],
        vaccineTypeName: ['', [Validators.required, Validators.maxLength(100)]],
        vaccineTypeCode: [{ value: '', disabled: true }, Validators.required],
        isActive: [false], // Ensure the default is set to false
        description: [''],
        image: []
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
    this.vaccineTypeId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadVaccine();
  }

  loadVaccine(): void {
    this.vaccineTypeService.getVaccineTypeById(this.vaccineTypeId).subscribe({
      next: (vaccineType) => {
        this.vaccineTypeData = vaccineType;
        this.vaccineTypeForm.patchValue({
          ...vaccineType,        
        });
        this.imageUrl = `${environment.BASE_API_URL}/api/image/ref/${ImageReference.VaccineType}_${vaccineType.id}`;
      },
      error: (error) => console.error('Error fetching vaccine type', error),
    });
  }

  onSubmit(): void {
    if (this.vaccineTypeForm.valid) {
      const updatedVaccineType: VaccineTypeDTO = {
        id: this.vaccineTypeId,
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
      
      console.log(request.getAll('Image'));

      this.vaccineTypeService.updateVaccineType(request).subscribe(
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
