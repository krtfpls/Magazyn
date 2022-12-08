import { Customer } from "./Customer";

export interface DocumentDto {
    id: string;
    type: string;
    customer: Customer | null;
    number: string;
    date: string;
}