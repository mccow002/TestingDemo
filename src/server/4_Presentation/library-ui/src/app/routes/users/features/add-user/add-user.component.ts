import {Component} from '@angular/core';
import {provideComponentStore} from "@ngrx/component-store";
import {AddUserStore} from "./add-user.store";
import {ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss'],
  imports: [
    ReactiveFormsModule
  ],
  providers: [provideComponentStore(AddUserStore)]
})
export class AddUserComponent {

  userForm = this.store.userForm;
  result$ = this.store.result$;

  constructor(private readonly store: AddUserStore) {
  }

  addUser() {
    this.store.addUser();
  }

  close() {
    this.store.close();
  }
}
