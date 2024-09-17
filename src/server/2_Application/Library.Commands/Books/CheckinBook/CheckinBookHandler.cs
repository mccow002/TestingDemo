using Library.Domain.Domain;
using Library.Models.Contracts;
using Library.Models.ViewModels;
using MediatR;

namespace Library.Commands.Books.CheckinBook;

public record CheckinBookRequest(Guid BookId) : IRequest<CheckinBookResponse>;

public record CheckinBookResponse(CheckoutViewModel? Checkout = null, Guid? FulfilledReservation = null);

public class CheckinBookHandler(IBookRepository bookRepository, TimeProvider timeProvider) : IRequestHandler<CheckinBookRequest, CheckinBookResponse>
{
    public async Task<CheckinBookResponse> Handle(CheckinBookRequest request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetBookForCheckin(request.BookId, cancellationToken);
        var currentCheckout = book.Checkouts.ElementAt(0);
        currentCheckout.Return(timeProvider);
        
        if(currentCheckout.ReturnDate > DateTime.Now)
        {
            var daysOverdue = (currentCheckout.ReturnDate.Value - currentCheckout.DueDate).TotalDays;
            currentCheckout.AddFine((decimal)(daysOverdue * 1.5), timeProvider);
        }

        if (book.Reservations.Count > 0)
        {
            var reservation = book.Reservations.OrderBy(x => x.HoldDate).ElementAt(0);
            reservation.Fulfill();
            bookRepository.Update(reservation);

            var checkout = Checkout.Create(book.BookId, reservation.UserId);
            bookRepository.Add(checkout);
            
            await bookRepository.SaveChangesAsync(cancellationToken);
            
            return new CheckinBookResponse(
                await bookRepository.GetCheckout(book.BookId, cancellationToken),
                reservation.ReservationId
            );
        }

        await bookRepository.SaveChangesAsync(cancellationToken);

        return new CheckinBookResponse();
    }
}