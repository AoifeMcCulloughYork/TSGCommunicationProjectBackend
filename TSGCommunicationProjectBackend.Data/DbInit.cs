using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TSGCommunicationProjectBackend.Data.Contexts;
using TSGCommunicationProjectBackend.Data.Entities;
using TSGCommunicationProjectBackend.Common;

namespace TSGCommunicationProjectBackend.Data;

public static class DbInit
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            // Apply migrations
            await context.Database.MigrateAsync();

            // Seed data
            await SeedDataAsync(context);
        }
        catch
        {
            throw;
        }
    }

    private static async Task SeedDataAsync(ApplicationDbContext context)
    {
        // Seed Communication Types
        if (!await context.CommunicationTypes.AnyAsync())
        {
            var communicationTypes = new[]
            {
                new CommunicationType { DisplayName = "EOB", Active = true },
                new CommunicationType { DisplayName = "EOP", Active = true },
                new CommunicationType { DisplayName = "ID Card", Active = true },
            };

            await context.CommunicationTypes.AddRangeAsync(communicationTypes);
            await context.SaveChangesAsync();
        }

        // Seed Communication Type Statuses
        if (!await context.CommunicationTypeStatuses.AnyAsync())
        {
            var typeStatuses = new[]
            {
                // EOB statuses (all applicable statuses)
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Created, Description = "EOB communication created" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.ReadyForRelease, Description = "EOB ready for release" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Released, Description = "EOB released" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.QueuedForPrinting, Description = "EOB queued for printing" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Printed, Description = "EOB printed" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Inserted, Description = "EOB inserted into envelope" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.WarehouseReady, Description = "EOB ready at warehouse" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Shipped, Description = "EOB shipped" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.InTransit, Description = "EOB in transit" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Delivered, Description = "EOB delivered" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Returned, Description = "EOB returned" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Failed, Description = "EOB failed" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Cancelled, Description = "EOB cancelled" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Expired, Description = "EOB expired" },
                new CommunicationTypeStatus { TypeCode = 1, StatusCode = Statuses.Denied, Description = "EOB denied" },
                
                // EOP statuses
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Created, Description = "EOP communication created" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.ReadyForRelease, Description = "EOP ready for release" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Released, Description = "EOP released" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.QueuedForPrinting, Description = "EOP queued for printing" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Printed, Description = "EOP printed" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Inserted, Description = "EOP inserted into envelope" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.WarehouseReady, Description = "EOP ready at warehouse" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Shipped, Description = "EOP shipped" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.InTransit, Description = "EOP in transit" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Delivered, Description = "EOP delivered" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Returned, Description = "EOP returned" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Failed, Description = "EOP failed" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Cancelled, Description = "EOP cancelled" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Expired, Description = "EOP expired" },
                new CommunicationTypeStatus { TypeCode = 2, StatusCode = Statuses.Denied, Description = "EOP denied" },
                
                // ID Card statuses
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Created, Description = "ID Card communication created" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.ReadyForRelease, Description = "ID Card ready for release" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Released, Description = "ID Card released" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.QueuedForPrinting, Description = "ID Card queued for printing" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Printed, Description = "ID Card printed" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Inserted, Description = "ID Card inserted into envelope" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.WarehouseReady, Description = "ID Card ready at warehouse" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Shipped, Description = "ID Card shipped" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.InTransit, Description = "ID Card in transit" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Delivered, Description = "ID Card delivered" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Returned, Description = "ID Card returned" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Failed, Description = "ID Card failed" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Cancelled, Description = "ID Card cancelled" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Expired, Description = "ID Card expired" },
                new CommunicationTypeStatus { TypeCode = 3, StatusCode = Statuses.Denied, Description = "ID Card denied" },
            };

            await context.CommunicationTypeStatuses.AddRangeAsync(typeStatuses);
            await context.SaveChangesAsync();
        }

        // Seed Members
        if (!await context.Members.AnyAsync())
        {
            var now = DateTime.UtcNow;
            var members = new[]
            {
                new Member
                {
                    Id = Guid.NewGuid(),
                    MemberId = "MEM001",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "+1-555-0101",
                    Address = "123 Main St",
                    ZipCode = "12345",
                    Active = true,
                    CreatedUtc = now,
                    LastUpdatedUtc = now
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    MemberId = "MEM002",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "+1-555-0102",
                    Address = "456 Oak Ave",
                    ZipCode = "12346",
                    Active = true,
                    CreatedUtc = now,
                    LastUpdatedUtc = now
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    MemberId = "MEM003",
                    FirstName = "Bob",
                    LastName = "Johnson",
                    Email = "bob.johnson@example.com",
                    PhoneNumber = "+1-555-0103",
                    Address = "789 Pine Rd",
                    ZipCode = "12347",
                    Active = true,
                    CreatedUtc = now,
                    LastUpdatedUtc = now
                }
            };

            await context.Members.AddRangeAsync(members);
            await context.SaveChangesAsync();

            // Seed Communications with actual member IDs
            var savedMembers = await context.Members.ToListAsync();
            var communications = new[]
            {
                new Communication
                {
                    Id = Guid.NewGuid(),
                    Title = "Welcome EOB",
                    TypeCode = 1, // EOB
                    CurrentStatus = Statuses.Delivered,
                    CreatedUtc = now.AddDays(-5),
                    LastUpdatedUtc = now.AddDays(-1),
                    Active = true,
                    MemberId = savedMembers[0].Id
                },
                new Communication
                {
                    Id = Guid.NewGuid(),
                    Title = "Monthly EOP Statement",
                    TypeCode = 2, // EOP
                    CurrentStatus = Statuses.InTransit,
                    CreatedUtc = now.AddDays(-3),
                    LastUpdatedUtc = now.AddHours(-6),
                    Active = true,
                    MemberId = savedMembers[1].Id
                },
                new Communication
                {
                    Id = Guid.NewGuid(),
                    Title = "Replacement ID Card",
                    TypeCode = 3, // ID Card
                    CurrentStatus = Statuses.Printed,
                    CreatedUtc = now.AddDays(-1),
                    LastUpdatedUtc = now.AddHours(-2),
                    Active = true,
                    MemberId = savedMembers[2].Id
                }
            };

            await context.Communications.AddRangeAsync(communications);
            await context.SaveChangesAsync();

            // Seed Communication Status History
            var savedCommunications = await context.Communications.ToListAsync();
            var statusHistories = new List<CommunicationStatusHistory>();

            // History for first communication (EOB - Welcome EOB)
            var comm1 = savedCommunications[0];
            statusHistories.AddRange(new[]
            {
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.Created, OccurredUtc = comm1.CreatedUtc },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.ReadyForRelease, OccurredUtc = comm1.CreatedUtc.AddHours(2) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.Released, OccurredUtc = comm1.CreatedUtc.AddHours(4) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.QueuedForPrinting, OccurredUtc = comm1.CreatedUtc.AddDays(1) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.Printed, OccurredUtc = comm1.CreatedUtc.AddDays(1).AddHours(6) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.Inserted, OccurredUtc = comm1.CreatedUtc.AddDays(2) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.WarehouseReady, OccurredUtc = comm1.CreatedUtc.AddDays(2).AddHours(4) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.Shipped, OccurredUtc = comm1.CreatedUtc.AddDays(3) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.InTransit, OccurredUtc = comm1.CreatedUtc.AddDays(3).AddHours(2) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm1.Id, StatusCode = Statuses.Delivered, OccurredUtc = comm1.LastUpdatedUtc }
            });

            // History for second communication (EOP - Monthly EOP Statement)
            var comm2 = savedCommunications[1];
            statusHistories.AddRange(new[]
            {
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.Created, OccurredUtc = comm2.CreatedUtc },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.ReadyForRelease, OccurredUtc = comm2.CreatedUtc.AddHours(1) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.Released, OccurredUtc = comm2.CreatedUtc.AddHours(3) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.QueuedForPrinting, OccurredUtc = comm2.CreatedUtc.AddDays(1) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.Printed, OccurredUtc = comm2.CreatedUtc.AddDays(1).AddHours(8) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.Inserted, OccurredUtc = comm2.CreatedUtc.AddDays(2) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.WarehouseReady, OccurredUtc = comm2.CreatedUtc.AddDays(2).AddHours(6) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.Shipped, OccurredUtc = comm2.CreatedUtc.AddDays(2).AddHours(12) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm2.Id, StatusCode = Statuses.InTransit, OccurredUtc = comm2.LastUpdatedUtc }
            });

            // History for third communication (ID Card - Replacement ID Card)
            var comm3 = savedCommunications[2];
            statusHistories.AddRange(new[]
            {
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm3.Id, StatusCode = Statuses.Created, OccurredUtc = comm3.CreatedUtc },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm3.Id, StatusCode = Statuses.ReadyForRelease, OccurredUtc = comm3.CreatedUtc.AddHours(1) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm3.Id, StatusCode = Statuses.Released, OccurredUtc = comm3.CreatedUtc.AddHours(4) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm3.Id, StatusCode = Statuses.QueuedForPrinting, OccurredUtc = comm3.CreatedUtc.AddHours(18) },
                new CommunicationStatusHistory { Id = Guid.NewGuid(), CommunicationId = comm3.Id, StatusCode = Statuses.Printed, OccurredUtc = comm3.LastUpdatedUtc }
            });

            await context.CommunicationStatusHistories.AddRangeAsync(statusHistories);
            await context.SaveChangesAsync();
        }
    }
}