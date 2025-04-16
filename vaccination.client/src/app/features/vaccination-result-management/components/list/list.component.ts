import { Component, OnInit, ViewChild } from '@angular/core';
import { VaccinationResultService } from '../../vaccination-result-management.service';
import { VaccinationResultDTO } from 'src/app/models/vaccinationResult/vaccinationResultDTO';
import { Router } from '@angular/router';
import { Table } from 'primeng/table';
import {
  ConfirmationService,
  MessageService,
  ConfirmEventType,
} from 'primeng/api';
import { Subscription } from 'rxjs';
@Component({
  selector: 'vaccination-result-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],

  providers: [VaccinationResultService]
})
export class VaccinationResultList implements OnInit {


  vaccinationResults!: VaccinationResultDTO[];
  selectedVaccinationResult!: VaccinationResultDTO[];
  subscriptions: Subscription[] = [];
  loading: boolean = true;
  keyword: string = '';

  @ViewChild('dt') dt: Table | undefined;
  constructor(private vaccinationResultService: VaccinationResultService,
    private router: Router, // Inject Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) { }

  search() {
    this.loading = true;
    this.vaccinationResultService.searchByKeyWord(this.keyword).subscribe((data) => {
      console.log(data)
      this.vaccinationResults = data.data;
      this.loading = false;
    })
  }

  ngOnInit() {
    this.getFunction();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe);
  }

  getFunction() {
    this.loading = true;
    this.vaccinationResultService.getAllVaccinationResults().subscribe((data) => {
      console.log(data)
      this.vaccinationResults = data.data;
      this.loading = false;
    })
  }

  createInjectionResult() {
    this.router.navigate(['/vaccination-result-management/add']);
  }

  deleteInjectionResult(vaccinationResult: VaccinationResultDTO) {
    console.log(vaccinationResult);
    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.subscriptions.push(
          this.vaccinationResultService.deleteVaccinationResult(vaccinationResult).subscribe(
            {
              next: (data) => {
                this.messageService.add({
                  severity: 'success',
                  summary: 'Success',
                  detail: 'Delete Success',
                });
                this.vaccinationResults = this.vaccinationResults.filter(vr => vr.id !== vaccinationResult.id);
                this.getFunction();
              },
            }
          )
        );
      },
    });
  }

  deleteManyInjectionResults() {
    this.confirmationService.confirm({
      message: 'Do you want to delete these records?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.subscriptions.push(
          this.vaccinationResultService.deleteManyVaccinationResult(this.selectedVaccinationResult).subscribe(
            {
              next: (data) => {
                this.messageService.add({
                  severity: 'success',
                  summary: 'Success',
                  detail: 'Delete Success',
                });
                const idsToDelete = new Set(this.selectedVaccinationResult.map(vr => vr.id));
                this.vaccinationResults = this.vaccinationResults.filter(vr => !idsToDelete.has(vr.id));
                this.getFunction();
              },
            }
          )
        );
      },
    });
  }

  updateInjectionResult(vaccinationResult: VaccinationResultDTO) {
    this.router.navigate(['/vaccination-result-management/update', vaccinationResult.id]);
  }
  applyFilterGlobal($event: any, stringVal: any) {
    console.log(($event.target as HTMLInputElement).value);
    console.log(stringVal);
    this.dt!.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
  }
}
