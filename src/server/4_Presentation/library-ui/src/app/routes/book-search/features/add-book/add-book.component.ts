import {Component} from '@angular/core';
import {AddBookStore} from "./add-book.store";
import {BookViewModel} from "../../../../shared/models";
import {provideComponentStore} from "@ngrx/component-store";
import {BookDetailsComponent} from "../../../../shared/book-details/book-details.component";
import {ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss'],
  imports: [
    CommonModule,
    BookDetailsComponent,
    ReactiveFormsModule
  ],
  providers: [provideComponentStore(AddBookStore)]
})
export class AddBookComponent {

  results$ = this.store.results$;
  searchCtrl = this.store.searchCtrl;

  constructor(
    private readonly store: AddBookStore) {
  }

  search() {
    this.store.searchBooks();
  }

  close() {
    this.store.close();
  }

  addBook(result: BookViewModel) {
    this.store.addBook(result.volumeId);
  }
}
