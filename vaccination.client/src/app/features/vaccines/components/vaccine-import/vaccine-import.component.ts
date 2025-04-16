import { Component } from '@angular/core';
import { VaccineService } from '../../services/vaccines.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
@Component({
  selector: 'app-vaccine-import',
  templateUrl: './vaccine-import.component.html',
  styleUrls: ['./vaccine-import.component.scss'],
})
export class VaccineImportComponent {
  selectedFile: File | null = null;

  constructor(private vaccineService: VaccineService, private router: Router) {}
  // Triggered when the file input changes
  onFileChange(event: any): void {
    console.log('file changed');
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  // Triggered when the user submits the form
  onImport(): void {
    if (this.selectedFile) {
      this.vaccineService.importVaccines(this.selectedFile).subscribe({
        next: (result) => {
          if (result.success) {
            // SweetAlert success message
            Swal.fire({
              title: 'Success',
              text: 'Vaccines imported successfully!',
              icon: 'success',
              confirmButtonText: 'OK',
            }).then(() => {
              // Navigate to vaccine home page after user clicks OK
              this.router.navigate(['/vaccines']);
            });
          } else {
            // SweetAlert error message
            const formattedErrorMessage = result.message.replace(/\n/g, '<br>');
            Swal.fire({
              title: 'Error',
              html: formattedErrorMessage,
              icon: 'error',
              confirmButtonText: 'OK',
            });
            this.resetForm();
          }
        },
        error: (error) => {
          // SweetAlert for any unexpected error
          Swal.fire({
            title: 'Error',
            text: 'An unexpected error occurred while importing vaccines.',
            icon: 'error',
            confirmButtonText: 'OK',
          });
          this.resetForm();
        },
      });
    }
  }

  resetForm(): void {
    this.selectedFile = null;
    const fileInput = document.getElementById('fileInput') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  cancelImport(): void {
    // You can navigate to a different route or reset form as per the requirement
    this.resetForm();
  }
}
