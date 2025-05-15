import {inject, Injectable} from '@angular/core';
import {HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {bookAddedNotification, BookViewModel, URL_TOKEN} from "./models";
import {Store} from "@ngrx/store";
import {from, of, switchMap} from "rxjs";
import {CatalogueItem} from "../routes/book-search/data-access/models";

@Injectable({
    providedIn: 'root'
})
export class SignalrService {

    baseUrl = inject(URL_TOKEN);
    store = inject(Store);

    hub = new HubConnectionBuilder()
        .withUrl(`${this.baseUrl}/notifications`, {
            withCredentials: false
        })
        .withAutomaticReconnect()
        .build();

    constructor() {
        this.hub.on('book-added', (msg: CatalogueItem) => {
            this.store.dispatch(bookAddedNotification({model: msg}));
        });
    }

    public start() {
        return of({}).pipe(
            switchMap(() => {
                if (this.hub?.state !== HubConnectionState.Connected) {
                    return from(this.hub!.start());
                }

                return of({});
            })
        );
    }

}
