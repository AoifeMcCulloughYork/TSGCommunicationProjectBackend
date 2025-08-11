using System;

namespace TSGCommunicationProjectBackend.Common.Interfaces;

public interface IEventPublisher
{
    Task PublishStatusChangeAsync(Guid CommunicationId, string newStatus, DateTime TimestampUtc);
}
