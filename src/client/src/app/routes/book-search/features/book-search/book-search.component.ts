import { Component } from '@angular/core';
import {BookCatalogueStore} from "./book-search.store";
import {provideComponentStore} from "@ngrx/component-store";

@Component({
  selector: 'app-book-search',
  templateUrl: './book-search.component.html',
  styleUrls: ['./book-search.component.scss'],
  providers: [provideComponentStore(BookCatalogueStore)]
})
export class BookSearchComponent {

  catalogue$ = this.store.catalogue$;

  constructor(private readonly store: BookCatalogueStore)
  {  }

  openAddInventory() {
    this.store.addBook();
  }

  checkout(bookId: string) {
    this.store.checkoutBook(bookId);
  }

  checkin(bookId: string) {
    
  }

  reserve(bookId: string) {
    
  }
}
