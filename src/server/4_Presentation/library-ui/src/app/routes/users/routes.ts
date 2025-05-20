import { Routes } from "@angular/router";

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/users/users.component').then(c => c.UsersComponent)
  }
]
