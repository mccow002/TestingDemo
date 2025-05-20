import {InjectionToken} from "@angular/core";
import {createAction, props} from "@ngrx/store";
import {CatalogueItem} from "../routes/book-search/data-access/models";

export const URL_TOKEN = new InjectionToken<string>('URL_TOKEN');

export type BookViewModel = {
  bookId: string;
  authorName: string;
  coverLink: string;
  publishDate: number;
  description: string;
  volumeId: string;
  publisher: string;
  title: string;
  subject: string;
};

export const bookAddedNotification = createAction(
  'BookAddedNotification',
  props<{model: CatalogueItem}>()
);
