import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

const routes: Routes = [
  {
    path: 'catalogue',
    loadChildren: () => import('./routes/book-search/book-search.module').then(m => m.BookSearchModule)
  },
  {
    path: 'users',
    loadChildren: () => import('./routes/users/users.module').then(m => m.UsersModule)
  },
  {path: '', redirectTo: 'catalogue', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
