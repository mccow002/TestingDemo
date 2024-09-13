import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookSearchComponent } from './features/book-search/book-search.component';
import {RouterModule, Routes} from "@angular/router";
import {BookDetailsComponent} from "../../shared/book-details/book-details.component";
import {AddBookComponent} from "./features/add-book/add-book.component";
import {ReactiveFormsModule} from "@angular/forms";

const routes: Routes = [
  {
    path: '',
    component: BookSearchComponent
  }
]

@NgModule({
  declarations: [
    BookSearchComponent,
    AddBookComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    BookDetailsComponent,
    ReactiveFormsModule
  ]
})
export class BookSearchModule { }
