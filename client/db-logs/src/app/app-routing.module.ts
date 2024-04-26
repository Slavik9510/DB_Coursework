import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatisticsViewerComponent } from './statistics-viewer/statistics-viewer.component';
import { LogsViewerComponent } from './logs-viewer/logs-viewer.component';
import { logsGuard } from './_guards/logs.guard';
import { statisticsGuard } from './_guards/statistics.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

const routes: Routes = [
  {
    path: 'statistics',
    component: StatisticsViewerComponent,
    canActivate: [statisticsGuard]
  },
  {
    path: 'logs',
    component: LogsViewerComponent,
    canActivate: [logsGuard]
  },
  { path: 'unauthorized', component: UnauthorizedComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
