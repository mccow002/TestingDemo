using System;
using System.Linq.Expressions;
using Library.Domain.Domain;
using Library.Models.ViewModels;

namespace CRA.Domain.Mappers
{
    public static partial class ReservationMapper
    {
        public static ReservationViewModel AdaptToViewModel(this Reservation p1)
        {
            return p1 == null ? null : new ReservationViewModel()
            {
                ReservationId = p1.ReservationId,
                HoldDate = p1.HoldDate,
                UserId = p1.UserId,
                Name = p1.User == null ? null : p1.User.Name
            };
        }
        public static Expression<Func<Reservation, ReservationViewModel>> ProjectToViewModel => p2 => new ReservationViewModel()
        {
            ReservationId = p2.ReservationId,
            HoldDate = p2.HoldDate,
            UserId = p2.UserId,
            Name = p2.User.Name
        };
    }
}