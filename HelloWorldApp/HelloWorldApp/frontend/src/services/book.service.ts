import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from 'src/models/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private apiUrl = 'https://localhost:5000/api/product/'; 

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    const url = this.apiUrl + 'books';
    return this.http.get<Book[]>(url);
  }
}
