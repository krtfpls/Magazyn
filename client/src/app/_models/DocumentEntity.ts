import { Customer } from "./Customer";
import { Product } from "./product";

export interface DocumentEntity {
    id: string;
    type: string;
    customer: Customer | undefined;
    number: string;
    date: string;
    productLines: Product[]
}