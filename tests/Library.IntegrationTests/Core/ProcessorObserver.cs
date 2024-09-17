using MassTransit;

namespace Library.IntegrationTests.Core;

public class ProcessorObserver(EventListener eventListener) : IConsumeObserver
{
    public Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        await eventListener.Handle(context.Message, context.CancellationToken);
    }

    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }
}