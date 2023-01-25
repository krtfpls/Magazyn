import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';
import { ProductClass, Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})

export class ProductCreateComponent implements OnInit {
  productForm: FormGroup = new FormGroup({});

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any) {
    if (this.productForm?.dirty) {
      $event.returnValue = true;
    }
  }
  
  id: string | undefined;  
  product: ProductClass = new ProductClass();
  validationErrors: string[] | undefined;
  categories: Category[] = [];

  
  constructor(private productService: ProductService, private route: ActivatedRoute, 
                private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.getCategories();
    this.initializeForm(this.product);
    this.route.params.subscribe(params =>
      this.id= params['id'])
    if (this.id){
        this.loadProduct(this.id);
      }
     
  }
  
  private initializeForm(product: Product) {
    this.productForm = this.fb.group({
      name: [product.name, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      priceNetto: [product.priceNetto, [Validators.required, Validators.min(0), Validators.max(9999)]],
      serialNumber: [product.serialNumber, [Validators.maxLength(50)]],
      minLimit: [product.minLimit, [Validators.min(0), Validators.max(9999)]],
      quantity: [product.quantity, []],
      description: [product.description, [Validators.maxLength(300)]],
      categoryName: [product.categoryName, [Validators.required]]
    });
  }

  get categoryProp() {
    return this.productForm.get('categoryName');
  }

  addProduct(){
   this.product = this.productForm.value;
   if (this.id){
    this.product.id= this.id;
    this.updateProduct(this.product);
   }
   else{
    this.createProduct(this.product);
  }
  }

  private createProduct(productToAdd: Product) {
    this.productService.createProduct(productToAdd).subscribe({
      next: _ => {
        this.toastr.success('Produkt utworzony');
        this.productForm?.reset(this.initializeForm(this.product = new ProductClass()));
      },
      error: error => {
        this.validationErrors = error;
      }
    });

    this.productService.clearProductsCache();
  }

  private getCategories(){
    this.productService.getCategories().subscribe({
      next: result => {
        this.categories= result;
      }
    })
  }

  private loadProduct(id: string) {
  
    this.productService.getProductDetail(id)?.subscribe({
      next: product => {
        this.product=product;
        this.productForm.reset(this.product)
    },
      error: error => console.log(error)
    });
  }

  private updateProduct(productToUpdate: Product){

    this.productService.updateProduct(productToUpdate).subscribe({
      next: _ => {
        this.toastr.success('Produkt zaktualizowany');
        return this.productForm?.reset(this.product);
      },
      error: error => {
        this.validationErrors = error
      }
    })

    this.productService.clearProductsCache();
  }
}
