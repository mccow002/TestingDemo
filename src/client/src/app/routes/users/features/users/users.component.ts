import { Component } from '@angular/core';
import {provideComponentStore} from "@ngrx/component-store";
import {UsersStore} from "./users.store";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  providers: [provideComponentStore(UsersStore)]
})
export class UsersComponent {

  users$ = this.store.users$;

  constructor(private readonly store: UsersStore)
  { }

  addUser(){
    this.store.openAddUser();
  }


}
