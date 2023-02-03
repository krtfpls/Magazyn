import { Customer } from "./Customer";
import { Product } from "./product";

export interface DocumentEntity {
    id: string;
    type: string;
    customer: Customer | undefined;
    number: string;
    date: string;
    documentLines: DocumentLine[]
}

export class DocumentLine {
  constructor ( public product: Product, public quantity: number) {}

  get total(): number {
    return this.product.priceNetto * this.quantity;

  }
}

export interface DocumentToSend {
  id: string;
  type: string;
  customerId: string;
  number: string;
  date: string;
  documentLines: DocumentLineToSend[]
}

export interface DocumentLineToSend {
  productId: string;
  quantity: number;
}