import {inject, Injectable} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";
import {UserViewModel} from "./models";
import {URL_TOKEN} from "../../../shared/models";

@Injectable({
  providedIn: 'root'
})
export class UsersHttpService {

  baseUrl = inject(URL_TOKEN);
  http = inject(HttpClient);

  getUsers(): Observable<UserViewModel[]> {
    return this.http.get<UserViewModel[]>(`${this.baseUrl}/users`);
  }

  addUser(name: string, email: string, cardNumber: string): Observable<UserViewModel> {
    return this.http.post<UserViewModel>(`${this.baseUrl}/users`, {
      name,
      email,
      cardNumber
    });
  }
}
