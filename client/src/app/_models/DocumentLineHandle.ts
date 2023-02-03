import { DocumentLine } from "./DocumentEntity";
import { Product } from "./product";

export class DocumentLineHandle {

  private lines = new Map<string, DocumentLine>();

  constructor(initialLines?: DocumentLine[]) {
    if (initialLines) {
      initialLines.forEach(dl => this.lines.set(dl.product.id, dl));
    }
  }

  public addProduct(prod: Product, quantity: number) {
    if (quantity === 0) {
      this.removeProduct(prod.id);
    }
    else {
      if (this.lines.has(prod.id)) {
        if (prod.serialNumber.length > 0)
          this.lines.get(prod.id)!.quantity = 1;
        else {
          const check = this.lines.get(prod.id)!.quantity += quantity;
          if (check === 0 || check <0)
            this.removeProduct(prod.id);
        }
      }
      else {
        if(quantity > 0)
        this.lines.set(prod.id, new DocumentLine(prod, quantity));
      }
    }
  }

  public clearAll(){
    this.lines.clear();
  }

  public removeProduct(id: string) {
    this.lines.delete(id);
  }

  get documentLines(): DocumentLine[] {
    return [...this.lines.values()];
  }

  get productCount(): number {
    return [...this.lines.values()]
      .reduce((total, dl) => total += dl.quantity, 0);
  }

  get total(): number {
    return [...this.lines.values()].reduce((total, dl) => total += dl.total, 0);
  }
}