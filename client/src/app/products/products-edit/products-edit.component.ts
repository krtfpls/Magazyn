import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-products-edit',
  templateUrl: './products-edit.component.html',
  styleUrls: ['./products-edit.component.css']
})
export class ProductsEditComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm | undefined;
  id: string | undefined;  
  product: Product | undefined;

  constructor(private productService: ProductService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.route.params.subscribe(params =>
      this.id= params['id']);
  
    this.productService.getProductDetail(this.id)?.subscribe({
      next: product => this.product=product,
      error: error => console.log(error)
    });
  }

  updateProduct(){
    this.productService.updateProduct(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Zaktualizowany');
        this.editForm?.reset(this.product);
      }
    })
  }
}
