using System;

namespace TSGCommunicationProjectBackend.Common.Interfaces;

public interface IRabbitMQConsumer
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
}
