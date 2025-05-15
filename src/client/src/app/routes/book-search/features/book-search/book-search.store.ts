import {inject, Injectable} from "@angular/core";
import { ComponentStore, OnStoreInit } from '@ngrx/component-store';
import { tapResponse } from '@ngrx/operators';

import {CatalogueHttpService} from "../../data-access/catalogue-http.service";
import {Observable, switchMap, tap} from "rxjs";
import {BsModalService} from "ngx-bootstrap/modal";
import {AddBookComponent} from "../add-book/add-book.component";
import {map} from "rxjs/operators";
import {CatalogueItem, CheckoutViewModel, ReservationViewModel} from "../../data-access/models";
import {createEntityAdapter, EntityAdapter, EntityState} from "@ngrx/entity";
import {ActionsSubject} from "@ngrx/store";
import {ofType} from "@ngrx/effects";
import {bookAddedNotification, BookViewModel} from "../../../../shared/models";

const catalogueAdapter: EntityAdapter<CatalogueItem> = createEntityAdapter<CatalogueItem>({
  selectId: model => model.bookId
});
type BookCatalogueState = EntityState<CatalogueItem>;


@Injectable()
export class BookCatalogueStore extends ComponentStore<BookCatalogueState> implements OnStoreInit {

  http = inject(CatalogueHttpService);
  modalService = inject(BsModalService);
  actions$ = inject(ActionsSubject);

  constructor() {
    super(catalogueAdapter.getInitialState());

    this.state$.subscribe(state => {
      console.log('State changed:', state);
    });
  }

  // selectors
  readonly catalogue$ = this.select(catalogueAdapter.getSelectors().selectAll);

  // updaters
  readonly setBooks = this.updater((state, catalogue: CatalogueItem[]) => catalogueAdapter.setAll(catalogue, state));

  readonly setCheckout = this.updater((state, value: { bookId: string, checkout: CheckoutViewModel | null }) =>
    catalogueAdapter.updateOne({
      id: value.bookId,
      changes: {
        checkout: value.checkout
      }
    }, state)
  );

  readonly addReservation = this.updater((state, value: { bookId: string, reservation: ReservationViewModel }) =>
    catalogueAdapter.updateOne({
      id: value.bookId,
      changes: {
        reservations: state.entities[value.bookId]!.reservations.concat(value.reservation)
      }
    }, state)
  );

  readonly removeReservation = this.updater((state, value: { bookId: string, reservationId: string }) =>
    catalogueAdapter.updateOne({
      id: value.bookId,
      changes: {
        reservations: state.entities[value.bookId]!.reservations.filter(x => x.reservationId !== value.reservationId)
      }
    }, state)
  );

  readonly addCatalogItem = this.updater((state, value: CatalogueItem) =>
    catalogueAdapter.addOne(value, state)
  );


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
      switchMap(data => this.http.checkoutBook(data.bookId, data.cardNumber).pipe(
        map(rsp => ({checkout: rsp, bookId: data.bookId}))
      )),
      tapResponse(
        rsp => this.setCheckout({bookId: rsp.bookId, checkout: rsp.checkout}),
        () => alert('Error Checking out!')
      )
    )
  );

  readonly reserveBook = this.effect((bookId$: Observable<string>) =>
    bookId$.pipe(
      map(bookId => ({
        bookId,
        cardNumber: prompt('Enter your card number') ?? ''
      })),
      switchMap(data => this.http.reserveBook(data.bookId, data.cardNumber).pipe(
        map(rsp => ({reservation: rsp, bookId: data.bookId}))
      )),
      tapResponse(
        rsp => this.addReservation({bookId: rsp.bookId, reservation: rsp.reservation}),
        () => alert('Error reserving book')
      )
    )
  );

  readonly checkinBook = this.effect((bookId$: Observable<string>) =>
    bookId$.pipe(
      switchMap(bookId => this.http.checkinBook(bookId).pipe(
        map(rsp => ({response: rsp, bookId: bookId}))
      )),
      tapResponse(
        data => {
          if (data.response.checkout) {
            this.setCheckout({bookId: data.bookId, checkout: data.response.checkout});
          } else {
            this.setCheckout({bookId: data.bookId, checkout: null});
          }

          if (data.response.fulfilledReservation) {
            this.removeReservation({bookId: data.bookId, reservationId: data.response.fulfilledReservation});
          }
        },
        () => alert('Error checking in book')
      )
    )
  );

  readonly onBookAdded = this.effect(() =>
    this.actions$.pipe(
      ofType(bookAddedNotification),
      tap(action => this.addCatalogItem(action.model))
    )
  );

  ngrxOnStoreInit() {
    this.getBooks();
  }
}
