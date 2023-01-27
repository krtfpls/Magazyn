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
    if (this.lines.has(prod.id)) {
      if (quantity === 0) {
        this.removeProduct(prod.id);
      } else {
        if (prod.serialNumber.length > 0)
          this.lines.get(prod.id)!.quantity = 1;
        else {
          const check = this.lines.get(prod.id)!.quantity += quantity;
          if (check === 0)
            this.removeProduct(prod.id);
        }
      }
    } else {
      this.lines.set(prod.id, new DocumentLine(prod, quantity));
    }
  }

  public removeProduct(id: string) {
    this.lines.delete(id);
  }

  public stepUpQty(id: string) {
    if (this.canStepUp(id))
      this.lines.get(id)!.quantity += 1;
  }

  public stepDownQty(id: string) {
    if (this.canStepDown(id))
      this.lines.get(id)!.quantity -= 1;
  }

  public canStepUp(id: string): boolean {

    return true
  }

  public canStepDown(id: string): boolean {
    return true;
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