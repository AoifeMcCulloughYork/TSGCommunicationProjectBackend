using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace TSGCommunicationProjectBackend.Data.Entities;

public class CommunicationStatusHistory
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid CommunicationId { get; set; }
    [Required]
    public string StatusCode { get; set; } = String.Empty;
    [Required]
    public DateTime OccurredUtc { get; set; }
}
