import { Author } from "./author";

export interface Book {
    id: number;
    title: string;
    year: number;
    author: Author;
}