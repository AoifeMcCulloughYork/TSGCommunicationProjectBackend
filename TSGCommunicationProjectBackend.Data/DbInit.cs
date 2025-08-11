using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TSGCommunicationProjectBackend.Data.Contexts;
using TSGCommunicationProjectBackend.Data.Entities;

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
                    new CommunicationType { DisplayName = "Email", Active = true },
                    new CommunicationType { DisplayName = "SMS", Active = true },
                    new CommunicationType { DisplayName = "Push Notification", Active = true },
                    new CommunicationType { DisplayName = "Letter", Active = true },
                    new CommunicationType { DisplayName = "Phone Call", Active = true }
                };

                await context.CommunicationTypes.AddRangeAsync(communicationTypes);
            }

            // Seed Communication Type Statuses
            if (!await context.CommunicationTypeStatuses.AnyAsync())
            {
                var typeStatuses = new[]
                {
                    // Email statuses
                    new CommunicationTypeStatus { TypeCode = 1, StatusCode = "DRAFT", Description = "Email draft created" },
                    new CommunicationTypeStatus { TypeCode = 1, StatusCode = "SENT", Description = "Email sent successfully" },
                    new CommunicationTypeStatus { TypeCode = 1, StatusCode = "DELIVERED", Description = "Email delivered" },
                    new CommunicationTypeStatus { TypeCode = 1, StatusCode = "OPENED", Description = "Email opened by recipient" },
                    new CommunicationTypeStatus { TypeCode = 1, StatusCode = "FAILED", Description = "Email delivery failed" },
                    
                    // SMS statuses
                    new CommunicationTypeStatus { TypeCode = 2, StatusCode = "DRAFT", Description = "SMS draft created" },
                    new CommunicationTypeStatus { TypeCode = 2, StatusCode = "SENT", Description = "SMS sent successfully" },
                    new CommunicationTypeStatus { TypeCode = 2, StatusCode = "DELIVERED", Description = "SMS delivered" },
                    new CommunicationTypeStatus { TypeCode = 2, StatusCode = "FAILED", Description = "SMS delivery failed" },
                    
                    // Push Notification statuses
                    new CommunicationTypeStatus { TypeCode = 3, StatusCode = "DRAFT", Description = "Push notification draft created" },
                    new CommunicationTypeStatus { TypeCode = 3, StatusCode = "SENT", Description = "Push notification sent" },
                    new CommunicationTypeStatus { TypeCode = 3, StatusCode = "DELIVERED", Description = "Push notification delivered" },
                    new CommunicationTypeStatus { TypeCode = 3, StatusCode = "FAILED", Description = "Push notification failed" },
                    
                    // Letter statuses
                    new CommunicationTypeStatus { TypeCode = 4, StatusCode = "DRAFT", Description = "Letter draft created" },
                    new CommunicationTypeStatus { TypeCode = 4, StatusCode = "PRINTED", Description = "Letter printed" },
                    new CommunicationTypeStatus { TypeCode = 4, StatusCode = "MAILED", Description = "Letter mailed" },
                    new CommunicationTypeStatus { TypeCode = 4, StatusCode = "DELIVERED", Description = "Letter delivered" },
                    
                    // Phone Call statuses
                    new CommunicationTypeStatus { TypeCode = 5, StatusCode = "SCHEDULED", Description = "Phone call scheduled" },
                    new CommunicationTypeStatus { TypeCode = 5, StatusCode = "COMPLETED", Description = "Phone call completed" },
                    new CommunicationTypeStatus { TypeCode = 5, StatusCode = "NO_ANSWER", Description = "No answer" },
                    new CommunicationTypeStatus { TypeCode = 5, StatusCode = "FAILED", Description = "Phone call failed" }
                };

                await context.CommunicationTypeStatuses.AddRangeAsync(typeStatuses);

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

                // Save members first to get their IDs
                await context.SaveChangesAsync();

                // Seed Communications with actual member IDs
                var savedMembers = await context.Members.ToListAsync();
                var communications = new[]
                {
                    new Communication
                    {
                        Id = Guid.NewGuid(),
                        Title = "Welcome Email",
                        TypeCode = 1, // Email
                        CurrentStatus = "SENT",
                        CreatedUtc = now.AddDays(-5),
                        LastUpdatedUtc = now.AddDays(-5),
                        Active = true,
                        MemberId = savedMembers[0].Id
                    },
                    new Communication
                    {
                        Id = Guid.NewGuid(),
                        Title = "Account Verification SMS",
                        TypeCode = 2, // SMS
                        CurrentStatus = "DELIVERED",
                        CreatedUtc = now.AddDays(-3),
                        LastUpdatedUtc = now.AddDays(-3),
                        Active = true,
                        MemberId = savedMembers[1].Id
                    },
                    new Communication
                    {
                        Id = Guid.NewGuid(),
                        Title = "Monthly Newsletter",
                        TypeCode = 1, // Email
                        CurrentStatus = "OPENED",
                        CreatedUtc = now.AddDays(-1),
                        LastUpdatedUtc = now.AddDays(-1),
                        Active = true,
                        MemberId = savedMembers[2].Id
                    }
                };

                await context.Communications.AddRangeAsync(communications);
            }

            // Save all changes
            var changes = await context.SaveChangesAsync();
        }
}
