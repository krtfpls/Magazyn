import { Location } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit {
  categoryForm: FormGroup = new FormGroup({});
  category: Category = {} as Category;
  validationErrors: string[] | undefined;
  @Output() CategoryEvent = new EventEmitter<Category>(); 
  @Output() backButtonEvent = new EventEmitter();

  constructor(private categoryService: CategoryService, private fb: FormBuilder, private location: Location,
      private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  public Create(){
    if (this.categoryForm.value != null)
    this.category= this.categoryForm.value;
    this.send(this.category);
  }

  public backButton(){
    this.backButtonEvent.emit();
  }

  private send(category: Category){
    this.categoryService.createCategory(category).subscribe({
      next: (id: any) => {
        category.id=id;
        this.CategoryEvent.emit(category);
        this.toastr.success('Kategoria utworzona poprawnie');
        this.categoryForm.reset(this.initializeForm);
      },
      error: error => {
        this.validationErrors = error;
      }
    });
  }

  private initializeForm() {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
    });
  }
}