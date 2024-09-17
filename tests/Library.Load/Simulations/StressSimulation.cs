using Library.Load.Scenarios;
using NBomber.Contracts;
using NBomber.CSharp;

namespace Library.Load.Simulations;

public class StressSimulation
{
    public static ScenarioProps Run(HttpClient client)
    {
        return LoadCatalogueScenario.Create("stress", client)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.RampingConstant(100, TimeSpan.FromMinutes(1)), // below Normal Load
                Simulation.KeepConstant(100, TimeSpan.FromMinutes(2)),
                Simulation.RampingConstant(200, TimeSpan.FromMinutes(1)), // normal load
                Simulation.KeepConstant(200, TimeSpan.FromMinutes(2)),
                Simulation.RampingConstant(300, TimeSpan.FromMinutes(1)), // beyond normal load
                Simulation.KeepConstant(300, TimeSpan.FromMinutes(2)),
                Simulation.RampingConstant(400, TimeSpan.FromMinutes(1)), // breaking point
                Simulation.KeepConstant(400, TimeSpan.FromMinutes(2))
            );
    }
}