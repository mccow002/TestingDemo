using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Library.CLI;

public class CommandLineHostApplicationBuilder : IHostApplicationBuilder
{
    public CommandLineHostApplicationBuilder(HostBuilderContext ctx, IServiceCollection services)
    {
        Properties = ctx.Properties;
        Configuration = new ConfigurationManager();
        Configuration.AddConfiguration(ctx.Configuration);
        Environment = ctx.HostingEnvironment;
        Services = services;
    }
    
    public IDictionary<object, object> Properties { get; set; }
    public IConfigurationManager Configuration { get; set; }
    public IHostEnvironment Environment { get; set; }
    public ILoggingBuilder Logging { get; set; }
    public IMetricsBuilder Metrics { get; set; }
    public IServiceCollection Services { get; set; }

    public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory,
        Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
    {
    }
}