import { Component, OnInit } from '@angular/core';
import { AnalyticsService } from '../../../services/analytics.service';
import { DashboardStats } from '../../../models/models';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  stats?: DashboardStats;
  loading = true;

  constructor(private analyticsService: AnalyticsService) {}

  ngOnInit(): void {
    this.analyticsService.getDashboardStats().subscribe({
      next: (stats) => {
        this.stats = stats;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
