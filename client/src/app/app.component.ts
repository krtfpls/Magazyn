import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Magazyn';
  Products: any;
  Documents: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getProducts();
    this.getDocuments();
  }

  getProducts() {
    this.http.get('http://localhost:5000/api/Products').subscribe(
      {
        next: response => this.Products = response,
        error: error => console.log(error)
      }
    )}
    getDocuments(){
    this.http.get('http://localhost:5000/api/Documents').subscribe(
      {
        next: response => this.Documents = response,
        error: error => console.log(error)
      }
    )
  }
}
