using System;
using TSGCommunicationProjectBackend.Common.Dtos.StatusDtos;

namespace TSGCommunicationProjectBackend.Common.Interfaces;

public interface IRabbitMQProducer
{
    //where T : class restricts T to reference types only, no ints etc
    Task PublishAsync<T>(T eventData, string routingKey = "") where T : class;
    Task PublishCommunicationStatusChangeAsync(CommunicationStatusChangedEvent eventData);
    Task PublishCommunicationCreatedAsync(CommunicationCreatedEvent eventData);
}
