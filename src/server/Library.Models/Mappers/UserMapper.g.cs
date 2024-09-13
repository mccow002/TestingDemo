using System;
using System.Linq.Expressions;
using Library.Domain.Domain;
using Library.Models.Users;

namespace CRA.Domain.Mappers
{
    public static partial class UserMapper
    {
        public static UserViewModel AdaptToViewModel(this User p1)
        {
            return p1 == null ? null : new UserViewModel()
            {
                UserId = p1.UserId,
                Name = p1.Name,
                Email = p1.Email,
                CardNumber = p1.CardNumber
            };
        }
        public static Expression<Func<User, UserViewModel>> ProjectToViewModel => p2 => new UserViewModel()
        {
            UserId = p2.UserId,
            Name = p2.Name,
            Email = p2.Email,
            CardNumber = p2.CardNumber
        };
    }
}