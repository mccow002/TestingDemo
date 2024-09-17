using AutoFixture;
using FluentAssertions;
using Library.Commands.Books.CheckinBook;
using Library.Commands.Tests.Books.CheckinBook.Fixtures;
using Library.Domain.Tests.Fakes;
using Library.Models.ViewModels;
using NSubstitute;

namespace Library.Commands.Tests.Books.CheckinBook;

public class CheckinBookTests
{
    public class Handle
    {
        private readonly CheckinBookHandlerFixture _fixture = new();

        [Fact]
        public async Task WhenCheckedInOnTimeWithNoReservations_ShouldCompleteCheckin()
        {
            // Arrange
            var request = _fixture.Create<CheckinBookRequest>();
            var sut = _fixture.CreateSut();

            // Act
            var rsp = await sut.Handle(request, default);

            // Assert
            rsp.Checkout.Should().BeNull();
            rsp.FulfilledReservation.Should().BeNull();
            
            _fixture.MockBookRepository.Received().GetBookForCheckin(request.BookId, default);
            _fixture.MockBookRepository.Received().SaveChangesAsync(default);
        }
        
        [Fact]
        public async Task WhenCheckedInOnTimeWithReservations_ShouldCompleteCheckinAndCheckoutNextReservation()
        {
            // Arrange
            var reservation = ReservationGenerator.Instance.Generate();
            _fixture.Book.Reservations.Add(reservation);

            var checkout = _fixture.Create<CheckoutViewModel>();
            _fixture.MockBookRepository.GetCheckout(Arg.Any<Guid>(), default)
                .Returns(checkout);
            
            var request = _fixture.Create<CheckinBookRequest>();
            var sut = _fixture.CreateSut();

            // Act
            var rsp = await sut.Handle(request, default);

            // Assert
            rsp.Checkout.Should().NotBeNull();
            rsp.Checkout.CheckoutTime.Should().Be(checkout.CheckoutTime);
            rsp.Checkout.Name.Should().Be(checkout.Name);
            rsp.Checkout.UserId.Should().Be(checkout.UserId);
            
            rsp.FulfilledReservation.Should().NotBeNull();
            rsp.FulfilledReservation.Should().Be(reservation.ReservationId);
            
            _fixture.MockBookRepository.Received()
                .GetBookForCheckin(request.BookId, default);
            
            _fixture.MockBookRepository.Received()
                .SaveChangesAsync(default);
        }
    }
}