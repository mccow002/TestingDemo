using Library.Load.Simulations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBomber.CSharp;

var client = new HttpClient();

var results = NBomberRunner
    .RegisterScenarios(
        LoadSimulation.Run(client)
        //StressSimulation.Run(client)
        //SpikeSimulation.Run(client)
        //SoakSimulation.Run(client)
    ).Run();

var loadStats = results.GetScenarioStats("load");
Assert.IsTrue(loadStats.Ok.Latency.Percent99 < 300);
Assert.IsTrue(loadStats.Ok.Request.RPS > 100);