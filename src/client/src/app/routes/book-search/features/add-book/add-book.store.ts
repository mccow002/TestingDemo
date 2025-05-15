import {Injectable} from "@angular/core";
import { ComponentStore } from '@ngrx/component-store';import { tapResponse } from '@ngrx/operators';

import {Observable, switchMap, tap} from "rxjs";
import {FormControl} from "@angular/forms";
import {BookViewModel} from "../../../../shared/models";
import {BsModalRef} from "ngx-bootstrap/modal";
import {CatalogueHttpService} from "../../data-access/catalogue-http.service";
import {SearchResults} from "../../data-access/models";

type AddBookState = {
  results: BookViewModel[];
  total: number;
}

@Injectable()
export class AddBookStore extends ComponentStore<AddBookState> {

  searchCtrl = new FormControl();

  constructor(
    private readonly modalRef: BsModalRef,
    private readonly http: CatalogueHttpService) {
    super({
      results: [],
      total: 0
    });
  }

  // selectors
  readonly results$ = this.select(state => state.results);
  readonly total$ = this.select(state => state.total);

  // updaters
  readonly setSearchResults = this.updater((state, results: SearchResults) => ({
    ...state,
    results: results.results,
    total: results.total
  }));

  // effects
  readonly searchBooks = this.effect((trigger$: Observable<void>) =>
    trigger$.pipe(
      switchMap(() => this.http.searchBooks(this.searchCtrl.value)),
      tap(results => this.setSearchResults(results))
    )
  );

  readonly addBook = this.effect((volumeId$: Observable<string>) =>
    volumeId$.pipe(
      switchMap(volumeId => this.http.addBook(volumeId)),
      tapResponse(
        () => this.modalRef.hide(),
        () => console.error('Failed to add book')
      )
    )
  );

  readonly close = this.effect((trigger$: Observable<void>) =>
    trigger$.pipe(
      tap(() => this.modalRef.hide())
    )
  );

}
