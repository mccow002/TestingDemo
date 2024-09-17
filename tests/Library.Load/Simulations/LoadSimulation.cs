using Library.Load.Scenarios;
using NBomber.Contracts;
using NBomber.CSharp;

namespace Library.Load.Simulations;

public class LoadSimulation
{
    public static ScenarioProps Run(HttpClient client)
    {
        return LoadCatalogueScenario.Create("load", client)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.RampingConstant(200, TimeSpan.FromSeconds(30)), // ramp up
                Simulation.KeepConstant(200, TimeSpan.FromMinutes(1)), // simulate expected load
                Simulation.RampingConstant(0, TimeSpan.FromSeconds(10)) // ramp down
            );
    }
}