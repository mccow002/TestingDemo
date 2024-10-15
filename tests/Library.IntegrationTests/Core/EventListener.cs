using System.Threading.Channels;

namespace Library.IntegrationTests.Core;

public class EventListener
{
    private readonly Channel<object> _events = Channel.CreateUnbounded<object>();

    public ChannelReader<object> Reader => _events.Reader;
    public ChannelWriter<object> Writer => _events.Writer;

    public async Task Handle(object @event, CancellationToken token)
    {
        await _events.Writer.WriteAsync(@event, token).ConfigureAwait(false);
    }

    public async Task WaitForProcessing<T>(CancellationToken token)
    {
        await foreach (var item in _events.Reader.ReadAllAsync(token).ConfigureAwait(false))
        {
            if (item.GetType() == typeof(T))
            {
                return;
            }

            if (item.GetType() == typeof(ErrorOccurredEvent))
            {
                throw ((ErrorOccurredEvent)item).Exception;
            }
        }
        
        throw new InvalidOperationException($"Event of type {typeof(T).Name} was not processed.");
    }
}

public record ErrorOccurredEvent(Exception Exception);