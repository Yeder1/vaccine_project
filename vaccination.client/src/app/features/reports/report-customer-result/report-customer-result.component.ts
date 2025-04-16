import { Component, ViewChild, ElementRef, AfterViewInit, OnInit } from '@angular/core';
import { ReportCustomerService, ReportCustomerDTO, ReportCustomerChartDTO } from '../services/report-customer.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-report-customer-result',
  templateUrl: './report-customer-result.component.html',
  styleUrls: ['./report-customer-result.component.scss']
})
export class ReportCustomerResultComponent implements OnInit, AfterViewInit {
  @ViewChild('customerChart') customerChart!: ElementRef;
  displayType: 'report' | 'chart' = 'report';
  reportCustomers: ReportCustomerDTO[] = [];
  selectedYear: number = new Date().getFullYear();
  years: number[] = [2021, 2022, 2023];
  dobFrom = '';
  dobTo = '';
  fullName = '';
  address = '';
  chart!: Chart;

  constructor(private reportCustomerService: ReportCustomerService) {}

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
    this.dobFrom = '';
    this.dobTo = '';
    this.fullName = '';
    this.address = '';
    this.loadReportData();
  }

  loadReportData() {
    this.reportCustomerService.getReportCustomers(this.dobFrom, this.dobTo, this.fullName, this.address).subscribe((data) => {
      this.reportCustomers = data;
    });
  }

  loadChartData() {
    this.reportCustomerService.getCustomerReportByYear(this.selectedYear).subscribe((data) => {
      const chartData = {
        labels: data.map(d => `Month ${d.month}`),
        datasets: [{
          label: 'Number of Customers',
          data: data.map(d => d.numberOfCustomers),
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1
        }]
      };

      if (this.chart) {
        this.chart.destroy();
      }

      this.chart = new Chart(this.customerChart.nativeElement, {
        type: 'bar',
        data: chartData,
        options: {
          responsive: true,
          scales: {
            y: { beginAtZero: true, title: { display: true, text: 'Number of Customers' }},
            x: { title: { display: true, text: 'Month' }}
          }
        }
      });
    });
  }
}
