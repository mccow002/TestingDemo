import {Injectable} from "@angular/core";
import {ComponentStore, OnStoreInit} from "@ngrx/component-store";
import {BookViewModel} from "../../../../shared/models";
import {CatalogueHttpService} from "../../data-access/catalogue-http.service";
import {Observable, switchMap, tap} from "rxjs";
import {BsModalService} from "ngx-bootstrap/modal";
import {AddBookComponent} from "../add-book/add-book.component";
import {map} from "rxjs/operators";
import {CatalogueItem} from "../../data-access/models";

type BookCatalogueState = {
  catalogue: CatalogueItem[]
}

@Injectable()
export class BookCatalogueStore extends ComponentStore<BookCatalogueState> implements OnStoreInit{
  constructor(
    private readonly http: CatalogueHttpService,
    private readonly modalService: BsModalService
  ) {
    super({catalogue: []});
  }

  // selectors
  readonly catalogue$ = this.select(state => state.catalogue);

  // updaters
  readonly setBooks = this.updater((state, catalogue: CatalogueItem[]) => ({
    ...state,
    catalogue
  }));

  // effects
  readonly getBooks = this.effect((trigger$: Observable<void>) =>
    trigger$.pipe(
      switchMap(() => this.http.getBooks()),
      tap(books => this.setBooks(books))
    )
  );

  readonly addBook = this.effect((trigger$: Observable<void>) =>
    trigger$.pipe(
      tap(() => this.modalService.show(AddBookComponent, {
        class: 'modal-lg'
      }))
    )
  );

  readonly checkoutBook = this.effect((bookId$: Observable<string>) =>
    bookId$.pipe(
      map(bookId => ({
        bookId,
        cardNumber: prompt('Enter your card number') ?? ''
      })),
      switchMap(data => this.http.checkoutBook(data.bookId, data.cardNumber)),
      //tap(rsp => )
    )
  );

  ngrxOnStoreInit() {
    this.getBooks();
  }
}
