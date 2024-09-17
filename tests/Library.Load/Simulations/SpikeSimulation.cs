using Library.Load.Scenarios;
using NBomber.Contracts;
using NBomber.CSharp;

namespace Library.Load.Simulations;

public class SpikeSimulation
{
    public static ScenarioProps Run(HttpClient client)
    {
        return LoadCatalogueScenario.Create("spike", client)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.RampingConstant(100, TimeSpan.FromSeconds(30)), // below Normal Load
                Simulation.KeepConstant(100, TimeSpan.FromMinutes(1)),
                Simulation.RampingConstant(1200, TimeSpan.FromMinutes(30)), // Major Spike
                Simulation.KeepConstant(1200, TimeSpan.FromMinutes(2)),
                Simulation.RampingConstant(300, TimeSpan.FromSeconds(30)), // scale down
                Simulation.KeepConstant(300, TimeSpan.FromMinutes(2))
            );
    }
}