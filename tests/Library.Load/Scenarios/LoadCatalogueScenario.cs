using NBomber.Contracts;
using NBomber.CSharp;

namespace Library.Load.Scenarios;

public class LoadCatalogueScenario
{
    public static Func<string, HttpClient, ScenarioProps> Create => (name, client) =>
        Scenario.Create(name, async context =>
        {
            var rsp = await client.GetAsync("https://localhost:7179/catalogue/books");
            return rsp.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
        });
}