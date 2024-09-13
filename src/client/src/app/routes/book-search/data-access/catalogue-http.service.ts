import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {BookViewModel} from "../../../shared/models";
import {CatalogueItem, SearchResults} from "./models";

@Injectable({
  providedIn: 'root'
})
export class CatalogueHttpService {

  baseUrl: string = 'https://localhost:7179';

  constructor(private readonly http: HttpClient) {
  }

  getBooks(): Observable<CatalogueItem[]> {
    return this.http.get<CatalogueItem[]>(`${this.baseUrl}/catalogue/books`);
  }

  searchBooks(searchTerm: string): Observable<SearchResults> {
    const params = { searchTerm };
    return this.http.get<SearchResults>(`${this.baseUrl}/books/search`, { params });
  }

  addBook(isbn: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/books`, { isbn });
  }

  checkoutBook(bookId: string, cardNumber: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/catalogue/checkout`, { bookId, cardNumber });
  }
}
