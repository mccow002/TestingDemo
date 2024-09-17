using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Library.Commands.Books.CheckinBook;
using Library.Domain.Domain;
using Library.Domain.Tests.Fakes;
using Library.Models.Contracts;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace Library.Commands.Tests.Books.CheckinBook.Fixtures;

public class CheckinBookHandlerFixture : Fixture
{
    public IBookRepository MockBookRepository { get; }
    public FakeTimeProvider MockTimeProvider { get; }

    public DateTimeOffset DateTimeNow { get; set; }

    public Book Book { get; set; }

    public CheckinBookHandlerFixture()
    {
        Customize(new AutoNSubstituteCustomization());

        MockBookRepository = this.Freeze<IBookRepository>();
        MockTimeProvider = new FakeTimeProvider();

        var bookGenerator = new BookGenerator();
        Book = bookGenerator.Generate();
        DateTimeNow = DateTimeOffset.Now;

        MockTimeProvider.SetUtcNow(DateTimeNow);
        MockBookRepository.GetBookForCheckin(Arg.Any<Guid>(), default).Returns(_ => Book);
    }

    public CheckinBookHandler CreateSut()
    {
        this.Inject<TimeProvider>(MockTimeProvider);
        return this.Create<CheckinBookHandler>();
    }
}