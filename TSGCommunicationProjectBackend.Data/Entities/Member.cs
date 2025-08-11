using System;
using System.ComponentModel.DataAnnotations;

namespace TSGCommunicationProjectBackend.Data.Entities;

public class Member
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string MemberId { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ZipCode { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime LastUpdatedUtc { get; set; }
    public List<Communication> Communications { get; set; } = new();
    
}
