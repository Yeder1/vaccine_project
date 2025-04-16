import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InjectionScheduleService } from '../../services/injection-schedule.service';
import { InjectionSchedule } from '../../models/injection-schedule.model';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  schedules: InjectionSchedule[] = [];
  filteredSchedules: InjectionSchedule[] = [];
  selectedScheduleId: number | null = null;
  selectedSchedule: InjectionSchedule | null = null; // For storing the selected schedule

  // Pagination properties
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalItems: number = 0;

  constructor(
    private injectionScheduleService: InjectionScheduleService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadSchedules();
  }

  loadSchedules(): void {
    this.injectionScheduleService.getAll().subscribe({
      next: (data) => {
        this.schedules = data;
        this.totalItems = data.length;
        this.updateFilteredSchedules();
      },
      error: (err) => {
        console.error('Error fetching schedules', err);
      }
    });
  }

  updateFilteredSchedules(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.filteredSchedules = this.schedules.slice(startIndex, endIndex);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.updateFilteredSchedules();
  }

  onItemsPerPageChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.itemsPerPage = +selectElement.value;
    this.currentPage = 1; // Reset to first page
    this.updateFilteredSchedules();
  }

  selectSchedule(schedule: InjectionSchedule): void {
    this.selectedScheduleId = schedule.id ?? null;
    this.selectedSchedule = schedule; // Store the selected schedule
    console.log('Selected schedule:', this.selectedSchedule);
  }

  navigateToAdd(): void {
    this.router.navigate(['/injection-schedule/add']);
  }

  navigateToUpdate(): void {
    if (this.selectedScheduleId) {
      this.router.navigate(['/injection-schedule/update', this.selectedScheduleId]);
    }
  }

  onSearch(event: Event): void {
    const searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    this.filteredSchedules = this.schedules.filter(schedule =>
      schedule.vaccineName?.toLowerCase().includes(searchTerm) ||
      schedule.place?.toLowerCase().includes(searchTerm) ||
      schedule.status?.toLowerCase().includes(searchTerm) ||
      schedule.description?.toLowerCase().includes(searchTerm) ||
      this.formatDate(schedule.startDate).includes(searchTerm) ||
      this.formatDate(schedule.endDate).includes(searchTerm)
    );
  }

  private formatDate(date: Date | undefined): string {
    if (!date) return '';
    const d = new Date(date);
    return d.toISOString().split('T')[0]; // Returns YYYY-MM-DD
  }

  getTotalPages(): number {
    return Math.ceil(this.totalItems / this.itemsPerPage);
  }

  getPagesArray(): number[] {
    return Array.from({length: this.getTotalPages()}, (_, i) => i + 1);
  }

  deleteSchedule() {
    if (this.selectedScheduleId) {
      if (confirm('Are you sure you want to delete this injection schedule?')) {
        this.injectionScheduleService.delete(this.selectedScheduleId).subscribe({
          next: () => {
            console.log('Injection schedule deleted successfully');
            this.loadSchedules(); // Refresh the list after deletion
            this.selectedScheduleId = null; // Reset the selection
          },
          error: (error) => {
            console.error('Error deleting injection schedule', error);
            // Handle error (e.g., show error message to user)
          }
        });
      }
    }
  }

  getStatusBackgroundColor(status: string): string {
    switch (status) {
      case 'NotYet':
        return '#fdd55c';  // Yellow
      case 'Open':
        return '#90EE90';  // Light Green
      case 'Over':
        return '#FFB6C1';  // Light Red
      default:
        return 'white';
    }
  }
}
