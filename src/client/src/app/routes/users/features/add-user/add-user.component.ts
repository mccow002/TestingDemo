import { Component } from '@angular/core';
import {provideComponentStore} from "@ngrx/component-store";
import {AddUserStore} from "./add-user.store";

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss'],
  providers: [provideComponentStore(AddUserStore)]
})
export class AddUserComponent {

  userForm = this.store.userForm;
  result$ = this.store.result$;

  constructor(private readonly store: AddUserStore)
  {  }


  protected readonly close = close;

  addUser() {
    this.store.addUser();
  }
}
