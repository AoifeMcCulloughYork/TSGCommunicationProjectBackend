using System;
using System.ComponentModel.DataAnnotations;

namespace TSGCommunicationProjectBackend.Data.Entities;

public class CommunicationType
{
    [Key]
    public int TypeCode { get; set; }
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    [Required]
    public bool Active { get; set; }
}