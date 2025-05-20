import {Component} from '@angular/core';
import {BookCatalogueStore} from "./book-search.store";
import {provideComponentStore} from "@ngrx/component-store";
import {BookDetailsComponent} from "../../../../shared/book-details/book-details.component";
import {CommonModule} from "@angular/common";
import {ModalModule} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-book-search',
  templateUrl: './book-search.component.html',
  styleUrls: ['./book-search.component.scss'],
  imports: [
    CommonModule,
    BookDetailsComponent
  ],
  providers: [
    provideComponentStore(BookCatalogueStore)
  ]
})
export class BookSearchComponent {

  catalogue$ = this.store.catalogue$;

  constructor(private readonly store: BookCatalogueStore) {
  }

  openAddInventory() {
    this.store.addBook();
  }

  checkout(bookId: string) {
    this.store.checkoutBook(bookId);
  }

  checkin(bookId: string) {
    this.store.checkinBook(bookId);
  }

  reserve(bookId: string) {
    this.store.reserveBook(bookId);
  }
}
