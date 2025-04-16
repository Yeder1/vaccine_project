import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ReportVaccineService } from '../services/report-vaccine.service';
import { ReportVaccineDTO } from '../models/ReportVaccineDTO';
import { Chart, ChartData, ChartOptions, registerables } from 'chart.js';

@Component({
  selector: 'app-report-vaccine-result',
  templateUrl: './report-vaccine-result.component.html',
  styleUrls: ['./report-vaccine-result.component.scss']
})
export class ReportVaccineResultComponent implements OnInit {
  reportVaccines: ReportVaccineDTO[] = [];
  injectionDateBegin?: string;
  injectionDateEnd?: string;
  vaccineType: string = '';
  origin: string = '';
  vaccineTypes = ['Type A', 'Type B', 'Type C'];

  displayType: 'report' | 'chart' = 'report';
  selectedYear: number = 2023;

  @ViewChild('vaccineChart') vaccineChart!: ElementRef;
  chart!: Chart;

  constructor(private reportVaccineService: ReportVaccineService) {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.loadReportVaccine();
  }

  loadReportVaccine(): void {
    this.reportVaccineService.getReportVaccine(this.injectionDateBegin, this.injectionDateEnd, this.vaccineType, this.origin).subscribe(
      (data) => {
        this.reportVaccines = data;
      },
      (error) => {
        console.error('Error fetching report vaccines:', error);
      }
    );
  }

  applyFilter(): void {
    this.loadReportVaccine();
  }

  resetFilter(): void {
    this.injectionDateBegin = undefined;
    this.injectionDateEnd = undefined;
    this.vaccineType = '';
    this.origin = '';
    this.loadReportVaccine();
  }

  onDisplayTypeChange(): void {
    if (this.displayType === 'chart') {
      this.loadChartData();
    }
  }
  private createOrUpdateChart(data: ChartData, options: ChartOptions): void {
    // Destroy the chart if it already exists
    if (this.chart) {
      this.chart.destroy();
    }

    // Create the chart
    this.chart = new Chart(this.vaccineChart.nativeElement, {
      type: 'bar',
      data: data,
      options: options
    });
  }

  private destroyChart(): void {
    if (this.chart) {
      this.chart.destroy();
      this.chart = undefined as any;
    }
  }

  loadChartData(): void {
    this.reportVaccineService.getVaccineReportByYear(this.selectedYear).subscribe(data => {
      const chartData: ChartData = {
        labels: data.map(d => `Month ${d.month}`),
        datasets: [{
          label: 'Number of Vaccines',
          data: data.map(d => d.numberOfVaccines),
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1
        }]
      };

      const chartOptions: ChartOptions = {
        responsive: true,
        scales: {
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Number of Vaccines'
            }
          },
          x: {
            title: {
              display: true,
              text: 'Month'
            }
          }
        }
      };

      this.createOrUpdateChart(chartData, chartOptions);
    });
  }
}
