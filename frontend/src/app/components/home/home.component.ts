import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { CategoryService } from '../../services/category.service';
import { Course, Category } from '../../models/models';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  featuredCourses: Course[] = [];
  categories: Category[] = [];
  loading = true;

  constructor(
    private courseService: CourseService,
    private categoryService: CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.courseService.getCourses().subscribe({
      next: (courses) => {
        this.featuredCourses = courses.slice(0, 6);
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });

    this.categoryService.getCategories().subscribe({
      next: (categories: Category[]) => {
        this.categories = categories;
      }
    });
  }

  viewCourse(courseId: number): void {
    this.router.navigate(['/courses', courseId]);
  }

  browseByCategory(categoryId: number): void {
    this.router.navigate(['/courses'], { queryParams: { category: categoryId } });
  }

  getCourseImage(course: Course): string {
    if (course.thumbnailUrl) {
      return course.thumbnailUrl;
    }
    // Return placeholder images from Unsplash
    const placeholders = [
      'https://images.unsplash.com/photo-1516321318423-f06f85e504b3?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1501504905252-473c47e087f8?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1488190211105-8b0e65b80b4e?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1546410531-bb4caa6b424d?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1522202176988-66273c2fd55f?w=600&h=400&fit=crop',
      'https://images.unsplash.com/photo-1504384308090-c894fdcc538d?w=600&h=400&fit=crop'
    ];
    const index = Math.abs(course.id) % placeholders.length;
    return placeholders[index];
  }
}
