using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
namespace TSGCommunicationProjectBackend.Data.Entities;

public class Communication
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = String.Empty;
    [Required]
    public int TypeCode { get; set; }
    [Required]
    public string CurrentStatus { get; set; } = String.Empty;
    [Required]
    public DateTime CreatedUtc { get; set; }
    [Required]
    public DateTime LastUpdatedUtc { get; set; }
    public bool Active { get; set; }
    [Required]
    public Guid MemberId { get; set; }
}
