import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './features/users/users.component';
import { AddUserComponent } from './features/add-user/add-user.component';
import {ReactiveFormsModule} from "@angular/forms";
import {BookDetailsComponent} from "../../shared/book-details/book-details.component";
import {RouterModule, Routes} from "@angular/router";

var routes: Routes = [
  {
    path: '',
    component: UsersComponent
  }
]

@NgModule({
  declarations: [
    UsersComponent,
    AddUserComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BookDetailsComponent,
    RouterModule.forChild(routes)
  ]
})
export class UsersModule { }
