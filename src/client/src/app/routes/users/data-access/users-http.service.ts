import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {UserViewModel} from "./models";

@Injectable({
  providedIn: 'root'
})
export class UsersHttpService {

  baseUrl: string = 'https://localhost:7179';

  constructor(private readonly http: HttpClient) {
  }

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
