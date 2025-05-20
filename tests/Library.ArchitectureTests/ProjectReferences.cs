using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Library.ArchitectureTests;

public class ProjectReferences
{
    private static List<Assembly> _domainAssemblies =
    [
        typeof(Domain.IDomainMarker).Assembly,
        typeof(Models.IModelsMarker).Assembly
    ];
    
    [Fact]
    public void DomainLayerReferences()
    {
        var result = Types.InAssemblies(_domainAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny("Library.OpenLibraryApi")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}