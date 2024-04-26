import { Component, OnInit } from '@angular/core';
import { ChartData } from 'chart.js';
import { StatisticsService } from '../_services/statistics.service';

@Component({
  selector: 'app-statistics-viewer',
  templateUrl: './statistics-viewer.component.html',
  styleUrls: ['./statistics-viewer.component.css']
})
export class StatisticsViewerComponent implements OnInit {
  barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: []
  };
  startDate: string | undefined;
  endDate: string | undefined;
  invalidDates: boolean = false;
  today: string = new Date().toISOString().split('T')[0];

  constructor(private statisticsService: StatisticsService) { }

  ngOnInit(): void {
    this.startDate = '2023-01-01';
    this.endDate = this.today;
    this.fetchChartData('sales-by-month', this.startDate, this.endDate);
  }

  validateDates(): void {
    if (this.startDate && this.endDate) {
      this.invalidDates = new Date(this.startDate) >= new Date(this.endDate);
    }
  }

  filter(value: string) {
    if (!this.startDate)
      this.startDate = '2023-01-01';

    if (!this.endDate)
      this.endDate = this.today;

    this.fetchChartData(value, this.startDate, this.endDate);
  }

  fetchChartData(endpoint: string, startDate: string, endDate: string) {
    let request;
    switch (endpoint) {
      case 'sales-by-month':
        request = this.statisticsService.getSalesByMonth(startDate, endDate);
        break;
      case 'city-orders-by-month':
        request = this.statisticsService.getCityOrdersByMonth(startDate, endDate);
        break;
      case 'avg-order-price-by-month':
        request = this.statisticsService.getAverageOrderPriceByMonth(startDate, endDate);
        break;
      case 'category-sales-by-month':
        request = this.statisticsService.getTotalSalesByCategoryAndMonth(startDate, endDate);
        break;
      case 'avg-customer-orders-by-month':
        request = this.statisticsService.getAvgOrdersPerCustomerByMonth(startDate, endDate);
        break;
      case 'products-returned-by-month':
        request = this.statisticsService.getProductsReturnedByMonth(startDate, endDate);
        break;
      case 'top-reviewed-products-by-month':
        request = this.statisticsService.getTopReviewedProductsByMonth(startDate, endDate);
        break;
      default:
        console.log('Unknown option');
        return;
    }

    request.subscribe(data => {
      this.barChartData = data;
    });
  }
}
