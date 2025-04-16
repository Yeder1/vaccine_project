import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InjectionScheduleService } from '../../services/injection-schedule.service';
import { InjectionSchedule } from '../../models/injection-schedule.model';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  schedule: InjectionSchedule | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private injectionScheduleService: InjectionScheduleService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadScheduleDetails(+id);
    }
  }

  loadScheduleDetails(id: number) {
    this.injectionScheduleService.getById(id).subscribe({
      next: (data) => {
        this.schedule = data;
      },
      error: (error) => {
        console.error('Error fetching injection schedule details', error);
      }
    });
  }

  onUpdate() {
    if (this.schedule) {
      this.router.navigate(['/injection-schedule/update', this.schedule.id]);
    }
  }

  onBack() {
    this.router.navigate(['/injection-schedule/list']);
  }
}
