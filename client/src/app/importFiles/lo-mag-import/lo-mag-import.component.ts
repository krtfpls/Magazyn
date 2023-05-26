import { Component, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-lo-mag-import',
  templateUrl: './lo-mag-import.component.html',
  styleUrls: ['./lo-mag-import.component.css']
})
export class LoMagImportComponent {
  file: File | undefined;
  workbook: XLSX.WorkBook | undefined;
  sheetData: any[][] | undefined;
  filteredRows: any[] = [];
  selectedValue: any;
  filteredData: any[] = [];
  buttonDisabled= true;


  onFileChange(event: any) {
    this.file = event.target.files[0];
  }

  importExcel(event: any) {
    this.file = event.target.files[0];
    
    if (this.file) {
      const fileReader = new FileReader();
      fileReader.onload = (e) => {
        const arrayBuffer: any = e.target?.result;
        const data = new Uint8Array(arrayBuffer);
        const arr = [];
        for (let i = 0; i !== data.length; ++i) arr[i] = String.fromCharCode(data[i]);
        const bstr = arr.join('');
        this.workbook = XLSX.read(bstr, { type: 'binary' });
        const worksheetName = this.workbook.SheetNames[0];
        const worksheet = this.workbook.Sheets[worksheetName];
        this.sheetData = XLSX.utils.sheet_to_json(worksheet, { header: 1 });
  
        // Utwórz listę wyboru bez powtórzeń na podstawie drugiej kolumny
        const columnIndex = 1; // Indeks drugiej kolumny (indeksowanie od 0)
        const columnData = this.sheetData.map((row) => row[columnIndex]);
        this.filteredRows = Array.from(new Set(columnData));
  
        this.filteredData = this.sheetData; // Wyświetlanie wszystkich danych na początku
      };
      fileReader.readAsArrayBuffer(this.file);
    }
  }
  
  
  applyFilter() {
    if (this.sheetData) {
      if (!this.selectedValue) {
        this.filteredData = this.sheetData; // Przypisanie pełnego zbioru danych
      } else {
        const columnIndex = 1; // Indeks drugiej kolumny (indeksowanie od 0)
        this.filteredData = this.sheetData.filter((row) => {
          const cellValue = row[columnIndex];
          return cellValue && cellValue.toLowerCase() === this.selectedValue.toLowerCase();
        });
      }
      this.buttonDisabled = false;
    }
  }
  
}