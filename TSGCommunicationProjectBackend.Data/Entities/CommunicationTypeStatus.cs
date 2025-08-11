using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
namespace TSGCommunicationProjectBackend.Data.Entities;

[PrimaryKey(nameof(TypeCode), nameof(StatusCode))]
public class CommunicationTypeStatus
{
    public int TypeCode { get; set; }
    public string StatusCode { get; set; } = String.Empty;
    [Required]
    public string Description { get; set; } = String.Empty;
}
