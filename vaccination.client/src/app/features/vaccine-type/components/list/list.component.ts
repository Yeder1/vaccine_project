import { Component, OnInit, ViewChild } from '@angular/core';
import { VaccineTypeDTO } from 'src/app/models/vaccineType/vacineTypeDTO';
import Swal from 'sweetalert2';
import { VaccineTypeService } from '../../services/vaccine-type.service';
import { Table } from 'primeng/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vaccine-type-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class VaccineTypeListComponent implements OnInit {
  vaccineTypes: VaccineTypeDTO[] = [];
  selectedVaccineTypes: VaccineTypeDTO[] = [];
  rows: number = 5; // Default number of entries shown per page
  rowsPerPageOptions: number[] = [5, 10, 25]; // Options for page size


  @ViewChild('table', { static: false }) table!: Table;

  constructor(private vaccineTypeService: VaccineTypeService, private router: Router) {}

  ngOnInit(): void {
    this.vaccineTypeService.getAllVaccineTypes().subscribe((data) => {
      this.vaccineTypes = data;
      console.log(this.vaccineTypes);
    });
  }

  createVaccineType(): void {
    this.router.navigate(['/vaccine-type/add']);
  }

  updateVaccineType(): void {
    if (this.selectedVaccineTypes.length === 1) {
      const selectedVaccineId = this.selectedVaccineTypes[0].id;
      this.router.navigate([`/vaccine-type/update/${selectedVaccineId}`]);
    }
  }

  deactivateVaccineTypes(): void {
    if (this.selectedVaccineTypes.length > 0) {
      // Show SweetAlert2 confirmation popup
      Swal.fire({
        title: 'Are you sure?',
        text: `You are about to make ${this.selectedVaccineTypes.length} vaccine types(s) inactive!`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, make them inactive!',
        cancelButtonText: 'No, cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          // Proceed with the deactivation if confirmed
          const selectedIds = this.selectedVaccineTypes
            .map((vaccine) => vaccine.id)
            .filter((id): id is number => id !== undefined);

          this.vaccineTypeService
            .deactivateVaccineTypes(this.selectedVaccineTypes)
            .subscribe(() => {
              Swal.fire(
                'Deactivated!',
                'The selected vaccine type(s) have been made inactive.',
                'success'
              );
              // Refresh the vaccine list
              this.vaccineTypeService.getAllVaccineTypes().subscribe((data) => {
                this.vaccineTypes = data;
                this.selectedVaccineTypes = [];
              });
            });
        }
      });
    }
  }

  applyFilterGlobal($event: any, stringVal: any) {
    this.table.filterGlobal(
      ($event.target as HTMLInputElement).value,
      stringVal
    );
  }

  clear(): void {
    this.table.clear(); // Clear the filters
  }
}


