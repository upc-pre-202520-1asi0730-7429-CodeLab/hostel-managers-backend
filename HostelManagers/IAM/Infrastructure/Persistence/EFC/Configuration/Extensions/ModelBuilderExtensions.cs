using HostelManagers.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context

        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>()
            .HasMany(u => u.Profiles)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
    }
}