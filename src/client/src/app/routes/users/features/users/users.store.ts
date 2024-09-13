import { Injectable } from '@angular/core';
import {ComponentStore, OnStoreInit} from '@ngrx/component-store';
import { UsersHttpService } from '../../data-access/users-http.service';
import {map, tap} from 'rxjs/operators';
import {Observable, switchMap} from "rxjs";
import {BsModalService} from "ngx-bootstrap/modal";
import {AddUserComponent} from "../add-user/add-user.component";
import {UserViewModel} from "../../data-access/models";

type UsersState = {
  users: UserViewModel[]
}

@Injectable()
export class UsersStore extends ComponentStore<UsersState> implements OnStoreInit{
  constructor(
    private readonly modalService: BsModalService,
    private usersHttpService: UsersHttpService) {
    super({ users: [] });
  }

  // Selectors
  readonly users$ = this.select(state => state.users);

  // Updaters
  readonly setUsers = this.updater((state, users: UserViewModel[]) => ({
    ...state,
    users
  }));

  readonly addUser = this.updater((state, user: UserViewModel) => ({
    ...state,
    users: [...state.users, user]
  }));

  // Effects
  readonly loadUsers = this.effect(trigger$ =>
    trigger$.pipe(
      switchMap(() => this.usersHttpService.getUsers()),
      tap(users => this.setUsers(users))
    )
  );

  readonly openAddUser = this.effect((trigger$: Observable<void>) =>
    trigger$.pipe(
      map(() => this.modalService.show(AddUserComponent, {
        class: 'modal-lg'
      })),
      switchMap(modalRef => modalRef.content!.result$),
      tap(result => this.addUser(result))
    )
  );

  ngrxOnStoreInit() {
    this.loadUsers();
  }
}
