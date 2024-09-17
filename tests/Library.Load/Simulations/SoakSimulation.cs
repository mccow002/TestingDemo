using Library.Load.Scenarios;
using NBomber.Contracts;
using NBomber.CSharp;

namespace Library.Load.Simulations;

public class SoakSimulation
{
    public static ScenarioProps Run(HttpClient client)
    {
        return LoadCatalogueScenario.Create("soak", client)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.RampingConstant(200, TimeSpan.FromMinutes(1)), // ramp up
                Simulation.KeepConstant(200, TimeSpan.FromHours(4)), // simulate expected load
                Simulation.RampingConstant(0, TimeSpan.FromMinutes(1)) // ramp down
            );
    }
}