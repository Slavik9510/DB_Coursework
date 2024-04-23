import { Component, OnInit } from '@angular/core';
import { ChartConfiguration } from 'chart.js';
import { StatisticsService } from '../_services/statistics.service';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit {
  public chartConfig: ChartConfiguration = {
    type: 'bar',
    data: {
      labels: [],
      datasets: []
    },
    options: {
      responsive: true,
      plugins: {
        legend: {
          display: true
        }
      }
    }
  };

  constructor(private statisticsService: StatisticsService) { }

  ngOnInit(): void {
    this.fetchChartData();
  }

  fetchChartData() {
    const startDate = '2023-01-01';
    const endDate = '2024-02-28';
    this.statisticsService.getSalesByMonth(startDate, endDate).subscribe({
      next: (data) => {
        this.chartConfig.data = data;
      }
    });
  }
}



/*public barChartData: ChartConfiguration['data'] = {
  labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
  datasets: [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' }
  ]
};
*/