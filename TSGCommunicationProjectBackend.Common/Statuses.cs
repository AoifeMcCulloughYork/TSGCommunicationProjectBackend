using System;

namespace TSGCommunicationProjectBackend.Common;
/*When adding a new status, it will need a new string here,
a new record for all applicable CommunicationTypes in CommunicationTypeStatus,
and a new RabbitMQ message. I should try to design the frontend to not need
new code for a new status in the event sim.
*/
public static class Statuses
{
    //Creation phase
    public const string Created = "Created";
    public const string ReadyForRelease = "ReadyForRelease";
    public const string Released = "Released";
    //Production phase
    public const string QueuedForPrinting = "QueuedForPrinting";
    public const string Printed = "Printed";
    public const string Inserted = "Inserted";
    public const string WarehouseReady = "WarehouseReady";
    //Logistics phase
    public const string Shipped = "Shipped";
    public const string InTransit = "InTransit";
    public const string Delivered = "Delivered";
    public const string Returned = "Returned";
    //Failure phase
    public const string Failed = "Failed";
    public const string Cancelled = "Cancelled";
    public const string Expired = "Expired";
    public const string Denied = "Denied";
}
