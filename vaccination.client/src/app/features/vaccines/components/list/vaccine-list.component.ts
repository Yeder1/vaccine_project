import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Table } from 'primeng/table';
import { VaccineService } from '../../services/vaccines.service';
import { VaccineDTO } from '../../models/vaccinceDTO';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vaccine-list',
  templateUrl: './vaccine-list.component.html',
  styleUrls: ['./vaccine-list.component.scss'],
})
export class VaccineListComponent implements OnInit {
  vaccines: VaccineDTO[] = [];
  selectedVaccines: VaccineDTO[] = [];
  rows: number = 5; // Default number of entries shown per page
  rowsPerPageOptions: number[] = [5, 10, 25]; // Options for page size

  @ViewChild('table', { static: false }) table!: Table;

  constructor(private vaccineService: VaccineService, private router: Router) {}

  ngOnInit(): void {
    this.vaccineService.getAllVaccines().subscribe((data) => {
      this.vaccines = data;
      console.log(this.vaccines);
    });
  }

  createVaccine(): void {
    this.router.navigate(['/vaccines/add']);
  }

  updateVaccine(): void {
    if (this.selectedVaccines.length === 1) {
      const selectedVaccineId = this.selectedVaccines[0].id;
      this.router.navigate([`/vaccines/update/${selectedVaccineId}`]);
    }
  }

  deactivateVaccines(): void {
    if (this.selectedVaccines.length > 0) {
      // Show SweetAlert2 confirmation popup
      Swal.fire({
        title: 'Are you sure?',
        text: `You are about to make ${this.selectedVaccines.length} vaccine(s) inactive!`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, make them inactive!',
        cancelButtonText: 'No, cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          // Proceed with the deactivation if confirmed
          const selectedIds = this.selectedVaccines
            .map((vaccine) => vaccine.id)
            .filter((id): id is number => id !== undefined);

          this.vaccineService
            .deactivateMultipleVaccines(selectedIds)
            .subscribe(() => {
              Swal.fire(
                'Deactivated!',
                'The selected vaccine(s) have been made inactive.',
                'success'
              );
              // Refresh the vaccine list
              this.vaccineService.getAllVaccines().subscribe((data) => {
                this.vaccines = data;
                this.selectedVaccines = [];
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
