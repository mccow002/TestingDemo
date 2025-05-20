import {BookViewModel} from "../../../shared/models";

export type SearchResults = {
  total: number;
  results: BookViewModel[];
}

export type CheckoutViewModel = {
  checkoutId: string;
  checkoutTime: Date;
  dueDate: Date;
  userId: string;
  name: string;
};

export type CatalogueItem = {
  bookId: string;
  checkout: CheckoutViewModel | null;
  book: BookViewModel;
  reservations: ReservationViewModel[];
}

export type ReservationViewModel = {
  reservationId: string;
  holdDate: Date;
  userId: string;
  name: string;
};

export type CheckinResponse = {
  checkout: CheckoutViewModel | null;
  fulfilledReservation: string | null;
}
