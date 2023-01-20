import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})

export class ProductsListComponent implements OnInit {

constructor(private router: Router) {}

  ngOnInit(): void { }

    editProductMode(id: string){
      this.router.navigateByUrl('/productsCreate/'+ id);
    }

}
