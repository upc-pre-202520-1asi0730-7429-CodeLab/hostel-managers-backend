using HostelManagers.Accounts.Domain.Model.Aggregates;
using HostelManagers.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Accounts.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Profiles Context
        builder.Entity<Profiles>().HasKey(p => p.Id);
        builder.Entity<Profiles>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profiles>().Property(p => p.Names).IsRequired().HasMaxLength(60);
        builder.Entity<Profiles>().Property(p => p.Roles).IsRequired();
        builder.Entity<Profiles>()
            .HasMany(p => p.Hotels)
            .WithOne(h => h.Profile)
            .HasForeignKey(h => h.ProfileId);
    }
}