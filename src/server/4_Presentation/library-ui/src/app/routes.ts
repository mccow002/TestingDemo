import {Routes} from "@angular/router";

export const routes: Routes = [
  {
    path: 'catalogue',
    loadChildren: () => import('./routes/book-search/routes').then(m => m.routes)
  },
  {
    path: 'users',
    loadChildren: () => import('./routes/users/routes').then(m => m.routes)
  },
  {path: '', redirectTo: 'catalogue', pathMatch: 'full'}
];
