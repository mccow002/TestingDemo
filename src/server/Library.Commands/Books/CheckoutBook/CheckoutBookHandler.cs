using Library.Domain.Domain;
using Library.Models.Contracts;
using Library.Models.ViewModels;
using MediatR;

namespace Library.Commands.Books.CheckoutBook;

public record CheckoutBookRequest(Guid BookId, string CardNumber) : IRequest<CheckoutViewModel>;

public class CheckoutBookHandler(IBookRepository bookRepository, IUserRepository userRepository) : IRequestHandler<CheckoutBookRequest, CheckoutViewModel>
{
    public async Task<CheckoutViewModel> Handle(CheckoutBookRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByLibraryCard(request.CardNumber, cancellationToken);
        
        var checkout = Checkout.Create(
            request.BookId,
            user.UserId,
            DateTime.Now
        );
        
        bookRepository.Add(checkout);
        await bookRepository.SaveChangesAsync(cancellationToken);

        return await bookRepository.GetCheckout(request.BookId, cancellationToken) ?? throw new InvalidOperationException();
    }
}