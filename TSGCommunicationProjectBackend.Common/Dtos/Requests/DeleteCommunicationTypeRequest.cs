using System;
using System.Data.Common;

namespace TSGCommunicationProjectBackend.Common.Dtos.Requests;

public class DeleteCommunicationTypeRequest
{
    public int TypeCode { get; set; }
}
