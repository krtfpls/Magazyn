import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/_models/category';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-list-category',
  templateUrl: './list-category.component.html',
  styleUrls: ['./list-category.component.css']
})
export class ListCategoryComponent implements OnInit {
  @Output() categoryEvent= new EventEmitter<Category>();
  categories: Category[] = [];
  page= 1;
  throttle = 0;
  distance = 2;

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getCategories();
  }

  private getCategories() {
    this.categoryService.getCategories().subscribe({
      next: result => {
        this.categories = result;
      }
    })
  }

  onScroll(){
    this.categoryService.getCategories(++this.page).subscribe({
      next: result => {
        this.categories.push(...result);
      }
    })
    console.log(this.categories)
  }

  chooseCategory(category: Category){
    this.categoryEvent.emit(category);
  }
}
