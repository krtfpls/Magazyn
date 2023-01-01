import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { newProduct } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent implements OnInit {
   newProductForm: FormGroup = new FormGroup({});
  id: string | undefined;  
  product: newProduct = new newProduct()  ;
  validationErrors: string[] | undefined;
  categories: Category[] = [];
  
  constructor(private productService: ProductService, private route: ActivatedRoute, 
                private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
    this.getCategories();
  }
  
  initializeForm() {
    this.newProductForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      priceNetto: [0.1, [Validators.required, Validators.min(0), Validators.max(9999)]],
      serialNumber: ['', [Validators.maxLength(50)]],
      minLimit: [1, [Validators.min(0), Validators.max(9999)]],
      quantity: [0, []],
      description: ['', [Validators.maxLength(300)]],
      categoryName: [null, [Validators.required]]
    });
  }

  get categoryProp() {
    return this.newProductForm.get('categoryName');
  }

  createProduct(){
   const productToAdd = this.newProductForm.value;

    this.productService.createProduct(productToAdd).subscribe({
      next: _ => {
        this.toastr.success('Produkt utworzony');
      
        this.product= new newProduct();
        this.newProductForm?.reset(this.product);
      },
      error: error => {
        this.validationErrors = error
      }
    })
  }

  getCategories(){
    this.productService.getCategories().subscribe({
      next: result => {
        this.categories= result;
      }
    })
  }
}
