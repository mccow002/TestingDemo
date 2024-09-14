using Library.Domain.Domain;
using Library.Models.Contracts;
using Library.Models.ViewModels;
using MediatR;

namespace Library.Commands.Books.CheckinBook;

public record CheckinBookRequest(Guid BookId) : IRequest<CheckinBookResponse>;

public record CheckinBookResponse(CheckoutViewModel? Checkout = null, Guid? FulfilledReservation = null);

public class CheckinBookHandler(IBookRepository bookRepository) : IRequestHandler<CheckinBookRequest, CheckinBookResponse>
{
    public async Task<CheckinBookResponse> Handle(CheckinBookRequest request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetBookForCheckin(request.BookId, cancellationToken);
        
        book.Checkouts.ElementAt(0).Return();

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