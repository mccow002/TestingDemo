using AutoFixture;
using FluentAssertions;
using Library.Commands.Books.CheckinBook;
using Library.Commands.Tests.Books.CheckinBook.Fixtures;
using Library.Domain.Tests.Fakes;
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
            
            var request = _fixture.Create<CheckinBookRequest>();
            var sut = _fixture.CreateSut();

            // Act
            var rsp = await sut.Handle(request, default);

            // Assert
            rsp.Checkout.Should().NotBeNull();
            rsp.FulfilledReservation.Should().NotBeNull();
            
            _fixture.MockBookRepository.Received().GetBookForCheckin(request.BookId, default);
            _fixture.MockBookRepository.Received().SaveChangesAsync(default);
        }
    }
}