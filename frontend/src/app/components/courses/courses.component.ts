import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../../services/course.service';
import { CategoryService } from '../../services/category.service';
import { Course, Category } from '../../models/models';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: Course[] = [];
  categories: Category[] = [];
  selectedCategoryId?: number;
  searchTerm = '';
  selectedLevel = '';
  loading = true;

  constructor(
    private courseService: CourseService,
    private categoryService: CategoryService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.selectedCategoryId = params['category'] ? +params['category'] : undefined;
      this.loadCourses();
    });

    this.categoryService.getCategories().subscribe({
      next: (categories: Category[]) => {
        this.categories = categories;
      }
    });
  }

  loadCourses(): void {
    this.loading = true;
    this.courseService.getCourses(this.selectedCategoryId, this.searchTerm, this.selectedLevel).subscribe({
      next: (courses) => {
        this.courses = courses;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.loadCourses();
  }

  onCategoryChange(categoryId: number | undefined): void {
    this.loadCourses();
  }

  onLevelChange(level: string): void {
    this.loadCourses();
  }

  viewCourse(courseId: number): void {
    // Navigate to course detail page
    console.log('View course:', courseId);
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
