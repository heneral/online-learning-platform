import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';
import {
  Chart,
  CategoryScale,
  LinearScale,
  BarController,
  LineController,
  DoughnutController,
  PointElement,
  LineElement,
  BarElement,
  ArcElement,
  Title,
  Tooltip,
  Legend,
  Filler,
  ChartConfiguration,
  ChartOptions
} from 'chart.js';

@Component({
  selector: 'app-overview',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './overview.html',
  styleUrl: './overview.css',
})
export class Overview {
  constructor() {
    console.log('Overview component loaded');

    // Register Chart.js components
    Chart.register(
      CategoryScale,
      LinearScale,
      BarController,
      LineController,
      DoughnutController,
      PointElement,
      LineElement,
      BarElement,
      ArcElement,
      Title,
      Tooltip,
      Legend,
      Filler
    );
  }

  // Enrollment Trends Line Chart
  public lineChartData: ChartConfiguration<'line'>['data'] = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        data: [65, 59, 80, 81, 56, 85],
        label: 'Enrollments',
        fill: true,
        tension: 0.4,
        borderColor: '#4f46e5',
        backgroundColor: 'rgba(79, 70, 229, 0.1)',
        pointBackgroundColor: '#4f46e5',
        pointBorderColor: '#ffffff',
        pointBorderWidth: 2,
        pointRadius: 6,
        pointHoverRadius: 8,
      }
    ]
  };

  public lineChartOptions: ChartOptions<'line'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'top'
      }
    },
    scales: {
      y: {
        beginAtZero: true,
        grid: {
          color: 'rgba(0, 0, 0, 0.1)'
        }
      },
      x: {
        grid: {
          display: false
        }
      }
    }
  };

  // Course Popularity Bar Chart
  public barChartData: ChartConfiguration<'bar'>['data'] = {
    labels: ['Angular', 'React', 'Vue', 'Node.js', 'Python', 'Java'],
    datasets: [
      {
        data: [45, 38, 25, 30, 35, 28],
        label: 'Students Enrolled',
        backgroundColor: [
          '#4f46e5',
          '#7c3aed',
          '#ec4899',
          '#f59e0b',
          '#10b981',
          '#06b6d4'
        ],
        borderRadius: 6,
        borderSkipped: false,
      }
    ]
  };

  public barChartOptions: ChartOptions<'bar'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'top'
      }
    },
    scales: {
      y: {
        beginAtZero: true,
        grid: {
          color: 'rgba(0, 0, 0, 0.1)'
        }
      },
      x: {
        grid: {
          display: false
        }
      }
    }
  };

  // User Distribution Doughnut Chart
  public doughnutChartData: ChartConfiguration<'doughnut'>['data'] = {
    labels: ['Students', 'Instructors'],
    datasets: [
      {
        data: [150, 25],
        backgroundColor: ['#4f46e5', '#f59e0b'],
        hoverBackgroundColor: ['#4338ca', '#d97706'],
        borderWidth: 0,
      }
    ]
  };

  public doughnutChartOptions: ChartOptions<'doughnut'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: 'bottom',
        labels: {
          padding: 20,
          usePointStyle: true,
        }
      }
    },
    cutout: '70%'
  };
}
