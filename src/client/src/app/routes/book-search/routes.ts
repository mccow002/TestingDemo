import {Routes} from "@angular/router";

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/book-search/book-search.component').then(c => c.BookSearchComponent)
  }
];
