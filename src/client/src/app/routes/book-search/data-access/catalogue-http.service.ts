import {inject, Injectable} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";
import {CatalogueItem, CheckinResponse, CheckoutViewModel, ReservationViewModel, SearchResults} from "./models";
import {URL_TOKEN} from "../../../shared/models";

@Injectable({
  providedIn: 'root'
})
export class CatalogueHttpService {

  baseUrl = inject(URL_TOKEN);
  http = inject(HttpClient);

  getBooks(): Observable<CatalogueItem[]> {
    return this.http.get<CatalogueItem[]>(`${this.baseUrl}/catalogue/books`);
  }

  searchBooks(searchTerm: string): Observable<SearchResults> {
    const params = { searchTerm };
    return this.http.get<SearchResults>(`${this.baseUrl}/books/search`, { params });
  }

  addBook(volumeId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/books`, { isbn: volumeId });
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
