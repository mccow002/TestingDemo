using AutoFixture;
using Library.Commands.Books.CheckinBook;
using Library.Commands.Tests.TUnit.Books.CheckinBook.Fixtures;
using Library.Domain.Tests.Fakes;
using Library.Models.ViewModels;
using NSubstitute;
using TUnit.Assertions.Extensions.Generic;

namespace Library.Commands.Tests.TUnit.Books.CheckinBook;

public class CheckinBookTests
{
    public class Handle
    {
        private CheckinBookHandlerFixture _fixture;
        
        [Before(Test)]
        public async Task SetupTest(TestContext context)
        {
            _fixture = new CheckinBookHandlerFixture();
        }

        [Test]
        public async Task WhenCheckedInOnTimeWithNoReservations_ShouldCompleteCheckin()
        {
            // Arrange
            var request = _fixture.Create<CheckinBookRequest>();
            var sut = _fixture.CreateSut();

            // Act
            var rsp = await sut.Handle(request, default);

            // Assert
            await Assert.That(rsp.Checkout).IsNull();
            await Assert.That(rsp.FulfilledReservation).IsNull();
            
            _fixture.MockBookRepository.Received().GetBookForCheckin(request.BookId, default);
            _fixture.MockBookRepository.Received().SaveChangesAsync(default);
        }
        
        [Test]
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
            await Assert.That(rsp.Checkout).IsNotNull();
            await Assert.That(rsp.Checkout.CheckoutTime).IsEqualTo(checkout.CheckoutTime);
            await Assert.That(rsp.Checkout.Name).IsEqualTo(checkout.Name);
            await Assert.That(rsp.Checkout.UserId).IsEqualTo(checkout.UserId);

            // using var _ = Assert.Multiple();
            //
            // await Assert.That(rsp.Checkout).IsNull();
            // await Assert.That(rsp.Checkout.CheckoutTime).IsEqualTo(checkout.CheckoutTime);
            // await Assert.That(rsp.Checkout.Name).IsEqualTo("Blergh");
            // await Assert.That(rsp.Checkout.UserId).IsEqualTo(checkout.UserId);
            
            await Assert.That(rsp.FulfilledReservation).IsNotNull();
            await Assert.That(rsp.FulfilledReservation).IsEqualTo(reservation.ReservationId);
            
            _fixture.MockBookRepository.Received()
                .GetBookForCheckin(request.BookId, default);
            
            _fixture.MockBookRepository.Received()
                .SaveChangesAsync(default);
        }
    }
}