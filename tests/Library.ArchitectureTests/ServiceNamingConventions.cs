using FluentAssertions;
using Library.Commands;
using Library.Queries;
using MediatR;
using NetArchTest.Rules;

namespace Library.ArchitectureTests;

public class ServiceNamingConventions
{
    [Fact]
    public void RequestsShouldUseRequestSuffix()
    {
        var result = Types.InAssemblies([typeof(ILibraryQueriesMarker).Assembly, typeof(ILibraryCommandsMarker).Assembly])
            .That()
            .ImplementInterface(typeof(IRequest))
            .Or()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Request")
            .GetResult();
        
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void RequestHandlersShouldUseHandlerSuffix()
    {
        var results = Types.InAssemblies([typeof(ILibraryQueriesMarker).Assembly, typeof(ILibraryCommandsMarker).Assembly])
            .That()
            .ImplementInterface(typeof(IRequestHandler<>))
            .Or()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        results.IsSuccessful.Should().BeTrue();
    }
}