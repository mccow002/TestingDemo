import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {BookViewModel} from "../../../shared/models";
import {CatalogueItem, CheckinResponse, CheckoutViewModel, ReservationViewModel, SearchResults} from "./models";

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

  checkoutBook(bookId: string, cardNumber: string): Observable<CheckoutViewModel> {
    return this.http.post<CheckoutViewModel>(`${this.baseUrl}/catalogue/checkout`, { bookId, cardNumber });
  }

  reserveBook(bookId: string, cardNumber: string): Observable<ReservationViewModel> {
    return this.http.post<ReservationViewModel>(`${this.baseUrl}/catalogue/reserve`, { bookId, cardNumber });
  }

  checkinBook(bookId: string): Observable<CheckinResponse> {
    return this.http.post<CheckinResponse>(`${this.baseUrl}/catalogue/checkin`, { bookId });
  }
}
