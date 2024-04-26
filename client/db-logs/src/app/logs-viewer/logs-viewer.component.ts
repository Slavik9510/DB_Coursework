
import { Component, OnInit } from '@angular/core';
import { LogService } from '../_services/log.service';

@Component({
  selector: 'app-logs-viewer',
  templateUrl: './logs-viewer.component.html',
  styleUrls: ['./logs-viewer.component.css']
})
export class LogsViewerComponent implements OnInit {
  logs: string[] = [];
  today: string = new Date().toISOString().split('T')[0];
  selectedDate: string = '';

  constructor(private logService: LogService) { }

  ngOnInit(): void {
  }

  onDateChange(newDate: string): void {
    this.selectedDate = newDate;
    this.fetchLogs();
  }

  fetchLogs(): void {
    this.logService.getLogs(this.selectedDate).subscribe(response => {
      this.logs = response;
    })
  }
}
