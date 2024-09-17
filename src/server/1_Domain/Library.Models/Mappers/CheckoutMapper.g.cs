using System;
using System.Linq.Expressions;
using Library.Domain.Domain;
using Library.Models.ViewModels;

namespace CRA.Domain.Mappers
{
    public static partial class CheckoutMapper
    {
        public static CheckoutViewModel AdaptToViewModel(this Checkout p1)
        {
            return p1 == null ? null : new CheckoutViewModel()
            {
                CheckoutId = p1.CheckoutId,
                CheckoutTime = p1.CheckoutTime,
                DueDate = p1.DueDate,
                UserId = p1.UserId,
                Name = p1.User == null ? null : p1.User.Name
            };
        }
        public static Expression<Func<Checkout, CheckoutViewModel>> ProjectToViewModel => p2 => new CheckoutViewModel()
        {
            CheckoutId = p2.CheckoutId,
            CheckoutTime = p2.CheckoutTime,
            DueDate = p2.DueDate,
            UserId = p2.UserId,
            Name = p2.User.Name
        };
    }
}