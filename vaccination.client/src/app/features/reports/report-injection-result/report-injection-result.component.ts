import { Component, ViewChild, ElementRef, AfterViewInit, OnInit } from '@angular/core';
import { ReportInjectionService, ReportInjectionDTO, ReportInjectionChartDTO } from '../services/report-injection.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-report-injection-result',
  templateUrl: './report-injection-result.component.html',
  styleUrls: ['./report-injection-result.component.scss']
})
export class ReportInjectionResultComponent implements OnInit, AfterViewInit {
  @ViewChild('injectionChart') injectionChart!: ElementRef;
  displayType: 'report' | 'chart' = 'report';
  reportInjections: ReportInjectionDTO[] = [];
  selectedYear: number = new Date().getFullYear();
  years: number[] = [2021, 2022, 2023];
  fromDate = '';
  toDate = '';
  prevention = '';
  vaccineTypeId = '';
  chart!: Chart;

  constructor(private reportInjectionService: ReportInjectionService) {}

  ngOnInit(): void {
    // Load report data with default empty filters when the page loads
    this.loadReportData();
  }

  ngAfterViewInit(): void {
    if (this.displayType === 'chart') {
      this.loadChartData();
    }
  }

  onDisplayTypeChange() {
    if (this.displayType === 'chart') {
      this.loadChartData();
    } else {
      this.loadReportData();
    }
  }

  filterReport() {
    this.loadReportData();
  }

  resetFilters() {
    this.fromDate = '';
    this.toDate = '';
    this.prevention = '';
    this.vaccineTypeId = '';
    this.loadReportData();
  }

  loadReportData() {
    this.reportInjectionService.getReportInjections(this.fromDate, this.toDate, this.prevention, this.vaccineTypeId).subscribe((data) => {
      this.reportInjections = data;
    });
  }

  loadChartData() {
    this.reportInjectionService.getInjectionReportByYear(this.selectedYear).subscribe((data) => {
      const chartData = {
        labels: data.map(d => `Month ${d.month}`),
        datasets: [{
          label: 'Number of Injections',
          data: data.map(d => d.numberOfInjections),
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1
        }]
      };

      if (this.chart) {
        this.chart.destroy();
      }

      this.chart = new Chart(this.injectionChart.nativeElement, {
        type: 'bar',
        data: chartData,
        options: {
          responsive: true,
          scales: {
            y: { beginAtZero: true, title: { display: true, text: 'Number of Injections' }},
            x: { title: { display: true, text: 'Month' }}
          }
        }
      });
    });
  }
}
