using System;
using System.Linq;
using System.Linq.Expressions;
using Library.Domain.Domain;
using Library.Models.ViewModels;

namespace CRA.Domain.Mappers
{
    public static partial class BookMapper
    {
        public static Expression<Func<Book, CatalogueItemViewModel>> ProjectToCatalogueItemViewModel => p1 => new CatalogueItemViewModel()
        {
            BookId = p1.BookId,
            Checkout = p1.Checkouts.FirstOrDefault<Checkout>(x => x.ReturnDate == null) == null ? null : new CheckoutViewModel()
            {
                CheckoutId = p1.Checkouts.FirstOrDefault<Checkout>(x => x.ReturnDate == null).CheckoutId,
                CheckoutTime = p1.Checkouts.FirstOrDefault<Checkout>(x => x.ReturnDate == null).CheckoutTime,
                DueDate = p1.Checkouts.FirstOrDefault<Checkout>(x => x.ReturnDate == null).DueDate,
                UserId = p1.Checkouts.FirstOrDefault<Checkout>(x => x.ReturnDate == null).UserId,
                Name = p1.Checkouts.FirstOrDefault<Checkout>(x => x.ReturnDate == null).User.Name
            },
            Reservations = p1.Reservations.Where<Reservation>(x => x.ReservationStatusId == ReservationStatus.Reserved.ReservationStatusId).Select<Reservation, ReservationViewModel>(p2 => new ReservationViewModel()
            {
                ReservationId = p2.ReservationId,
                HoldDate = p2.HoldDate,
                UserId = p2.UserId,
                Name = p2.User.Name
            }).ToList<ReservationViewModel>()
        };
    }
}