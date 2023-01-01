import { Customer } from "./Customer";

export interface DocumentDto {
    id: string;
    type: string;
    customer: Customer | undefined;
    number: string;
    date: string;
}