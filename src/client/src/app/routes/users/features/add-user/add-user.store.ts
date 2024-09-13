import {Injectable} from "@angular/core";
import {ComponentStore} from "@ngrx/component-store";
import {FormBuilder, Validators} from "@angular/forms";
import {UsersHttpService} from "../../data-access/users-http.service";
import {filter, Observable, Subject, switchMap, tap} from "rxjs";
import {BsModalRef} from "ngx-bootstrap/modal";
import {UserViewModel} from "../../data-access/models";

type AddUserState = {}


@Injectable()
export class AddUserStore extends ComponentStore<AddUserState> {

  userForm = this.fb.group({
    name: this.fb.control('', [Validators.required]),
    email: this.fb.control('', [Validators.required, Validators.email]),
    cardNumber: this.fb.control('', [Validators.required])
  });

  result$ = new Subject<UserViewModel>();

  constructor(
    private readonly usersHttpService: UsersHttpService,
    private readonly fb: FormBuilder,
    private readonly modalRef: BsModalRef
  ) {
    super({})
  }

  // effects
  readonly addUser = this.effect((trigger$: Observable<void>) =>
    trigger$.pipe(
      filter(() => this.userForm.valid),
      switchMap(() => this.usersHttpService.addUser(
          this.userForm.get('name')!.value ?? '',
          this.userForm.get('email')!.value ?? '',
          this.userForm.get('cardNumber')!.value ?? ''
        )
      ),
      tap(rsp => {
        this.result$.next(rsp);
        this.modalRef.hide();
      })
    )
  );
}
