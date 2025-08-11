using System;

namespace TSGCommunicationProjectBackend.Common.Dtos.StatusDtos;

public class CommunicationEventBaseDto
{
    /*nearly all (currently all except Created) event messages follow this exact format
    They're broken up into separate Dtos for a couple reasons:
    In the future, if i.e. the Shipped Dto needs to add a tracking number or something
    it'll be easy to find and modify that specific event contract.
    It also more closely complies with the Event contract in the project spec
    without requiring a Dto for each possible communication type and event together,
    which would rapidlyh balloon out of control. Since adding a new status
    already requires many (but consistent) code changes, I feel that
    creating a new Dto for it is not a big ask, but creating a new Dto for all
    possible combinations of type + status would be egregious.
    */
    public Guid CommunicationId { get; set; }
    public DateTime TimestampUtc { get; set; }
    public string EventType { get; set; } = String.Empty;
}
