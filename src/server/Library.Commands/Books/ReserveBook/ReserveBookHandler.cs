using Library.Domain.Domain;
using Library.Models.Contracts;
using Library.Models.ViewModels;
using MediatR;

namespace Library.Commands.Books.ReserveBook;

public record ReserveBookRequest(Guid BookId, string CardNumber) : IRequest<ReservationViewModel>;

public class ReserveBookHandler(IBookRepository bookRepository, IUserRepository userRepository) : IRequestHandler<ReserveBookRequest, ReservationViewModel>
{
    public async Task<ReservationViewModel> Handle(ReserveBookRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByLibraryCard(request.CardNumber, cancellationToken);
        
        var reservation = Reservation.Create(request.BookId, user.UserId);
        bookRepository.Add(reservation);
        await bookRepository.SaveChangesAsync(cancellationToken);

        return await bookRepository.GetReservation(reservation.ReservationId, cancellationToken) ?? throw new InvalidOperationException();
    }
}