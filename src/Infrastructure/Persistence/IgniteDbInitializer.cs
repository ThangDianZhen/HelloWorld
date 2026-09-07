using Ignite.Application.Configs;
using Ignite.Core.Constants;
using Ignite.Core.Entities;
using Ignite.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ignite.Infrastructure.Persistence;

public static class IgniteDbInitializer
{
    public static async Task InitializeAsync(AppDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IgniteOptions options)
    {
        await db.Database.EnsureCreatedAsync();
        await SeedIdentityAsync(userManager, roleManager, options);
        await SeedContentAsync(db, options);
    }

    private static async Task SeedIdentityAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IgniteOptions options)
    {
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }

        var admin = await userManager.FindByEmailAsync(options.AdminEmail);
        if (admin is null)
        {
            admin = new IdentityUser
            {
                UserName = options.AdminEmail,
                Email = options.AdminEmail,
                EmailConfirmed = true
            };

            var created = await userManager.CreateAsync(admin, options.AdminPassword);
            if (created.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }
        else if (!await userManager.IsInRoleAsync(admin, Roles.Admin))
        {
            await userManager.AddToRoleAsync(admin, Roles.Admin);
        }
    }

    private static async Task SeedContentAsync(AppDbContext db, IgniteOptions options)
    {
        if (await db.ChurchSettings.AnyAsync())
        {
            return;
        }

        var klId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var sgId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var onlineId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        db.ChurchSettings.Add(new ChurchSettings(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"))
        {
            Name = options.ChurchName,
            Tagline = "One church. Many cities. Jesus at the centre.",
            HeroTitle = "A church for your city",
            HeroSubtitle = "Join us this weekend for worship, the Word, and a family that will walk with you.",
            LivestreamUrl = "https://www.youtube.com/@IgniteChurch",
            GivingUrl = "https://tithe.ly",
            GivingHeadline = "Give online",
            GivingBody = "Your generosity helps us serve our cities, support families, and bring the hope of Jesus to people who need it.",
            ContactEmail = "hello@ignitechurch.local",
            ContactPhone = "+60 12-000 0000",
            FooterNote = "Ignite Church is a local church family helping people follow Jesus.",
            AboutHtml = "<p>Ignite exists to help people encounter Jesus, grow as disciples, and serve their city. We are one church meeting in multiple locations, with the same heart: to make the love of God real in everyday life.</p>"
        });

        var kl = new Campus(klId)
        {
            Name = "Ignite Kuala Lumpur",
            Slug = "kuala-lumpur",
            City = "Kuala Lumpur",
            Region = "Wilayah Persekutuan",
            Country = "Malaysia",
            Address = "City Centre, Kuala Lumpur",
            Description = "Our KL family gathers every Sunday with kids programmes and midweek connect groups.",
            ContactEmail = "kl@ignitechurch.local",
            IsPublished = true,
            SortOrder = 1
        };
        kl.ServiceTimes.Add(new ServiceTime { Day = ServiceDay.Sunday, TimeLabel = "10:00 AM", Title = "Main Service", SortOrder = 1 });
        kl.ServiceTimes.Add(new ServiceTime { Day = ServiceDay.Sunday, TimeLabel = "10:00 AM", Title = "Ignite Kids", Notes = "Birth to 12 years", SortOrder = 2 });
        kl.ServiceTimes.Add(new ServiceTime { Day = ServiceDay.Wednesday, TimeLabel = "8:00 PM", Title = "Prayer Meeting", SortOrder = 3 });

        var sg = new Campus(sgId)
        {
            Name = "Ignite Singapore",
            Slug = "singapore",
            City = "Singapore",
            Region = "Central",
            Country = "Singapore",
            Address = "City Hall, Singapore",
            Description = "Join us in the heart of the city for weekend services and youth.",
            ContactEmail = "sg@ignitechurch.local",
            IsPublished = true,
            SortOrder = 2
        };
        sg.ServiceTimes.Add(new ServiceTime { Day = ServiceDay.Sunday, TimeLabel = "11:00 AM", Title = "Main Service", SortOrder = 1 });
        sg.ServiceTimes.Add(new ServiceTime { Day = ServiceDay.Friday, TimeLabel = "7:30 PM", Title = "Youth", SortOrder = 2 });

        var online = new Campus(onlineId)
        {
            Name = "Ignite Online",
            Slug = "online",
            City = "Anywhere",
            Region = string.Empty,
            Country = "Worldwide",
            Address = "Livestream",
            Description = "Watch from anywhere. Weekly services, kids content, and a podcast to stay connected.",
            IsOnlineCampus = true,
            IsPublished = true,
            SortOrder = 3
        };
        online.ServiceTimes.Add(new ServiceTime { Day = ServiceDay.Sunday, TimeLabel = "11:00 AM GMT+8", Title = "Livestream", SortOrder = 1 });

        db.Campuses.AddRange(kl, sg, online);

        db.Sermons.AddRange(
            new Sermon
            {
                Title = "A Faith That Moves",
                Speaker = "Ps. Daniel Tan",
                Series = "Unshakeable",
                PreachedOn = DateTime.UtcNow.Date.AddDays(-7),
                VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                Description = "What does it look like to trust God when the path is unclear?",
                CampusId = klId,
                IsPublished = true
            },
            new Sermon
            {
                Title = "Family on Mission",
                Speaker = "Ps. Sarah Lim",
                Series = "House to House",
                PreachedOn = DateTime.UtcNow.Date.AddDays(-14),
                Description = "God builds His church through ordinary families who love people well.",
                CampusId = sgId,
                IsPublished = true
            },
            new Sermon
            {
                Title = "Stay On Fire",
                Speaker = "Ps. Daniel Tan",
                Series = "Ignite",
                PreachedOn = DateTime.UtcNow.Date.AddDays(-21),
                Description = "Keep your heart soft, your faith active, and your life available to Jesus.",
                CampusId = onlineId,
                IsPublished = true
            });

        db.Events.AddRange(
            new ChurchEvent
            {
                Title = "Welcome Weekend",
                Slug = "welcome-weekend",
                Description = "New to Ignite? Come meet the team, tour kids check-in, and find your next step.",
                StartsAtUtc = DateTime.UtcNow.Date.AddDays(14).AddHours(2),
                LocationName = "Ignite Kuala Lumpur",
                CampusId = klId,
                IsPublished = true
            },
            new ChurchEvent
            {
                Title = "Night of Worship",
                Slug = "night-of-worship",
                Description = "An evening of worship and prayer for our cities.",
                StartsAtUtc = DateTime.UtcNow.Date.AddDays(21).AddHours(11),
                LocationName = "Ignite Singapore",
                CampusId = sgId,
                IsPublished = true
            });

        db.Ministries.AddRange(
            new Ministry { Name = "Kids", Slug = "kids", Description = "A safe, fun environment where children discover Jesus in an age-appropriate way.", SortOrder = 1, IsPublished = true },
            new Ministry { Name = "Youth", Slug = "youth", Description = "A community for teenagers to grow in faith, friendship, and purpose.", SortOrder = 2, IsPublished = true },
            new Ministry { Name = "Connect Groups", Slug = "connect-groups", Description = "Small groups that meet during the week to pray, study, and do life together.", SortOrder = 3, IsPublished = true },
            new Ministry { Name = "Worship", Slug = "worship", Description = "Musicians, singers, and production volunteers who help our church encounter God.", SortOrder = 4, IsPublished = true });

        db.Pages.AddRange(
            new Page
            {
                Title = "About",
                Slug = "about",
                Summary = "Who we are and why we exist.",
                BodyHtml = "<p>Ignite is a church family for people who want a real relationship with Jesus. We gather on weekends, grow in groups, and serve our cities through practical love.</p><p>Whether you are exploring faith or have walked with God for years, there is a place for you here.</p>",
                SortOrder = 1,
                IsPublished = true
            },
            new Page
            {
                Title = "Plan a visit",
                Slug = "visit",
                Summary = "What to expect this Sunday.",
                BodyHtml = "<p>Come as you are. Services last about 90 minutes and include worship, a message, and kids programmes. Parking and check-in teams will help you feel at home from the moment you arrive.</p>",
                SortOrder = 2,
                IsPublished = true
            });

        await db.SaveChangesAsync();
    }
}
