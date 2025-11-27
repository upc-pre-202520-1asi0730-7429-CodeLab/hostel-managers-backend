using HostelManagers.Hotels.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Hotels.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHotelsConfiguration(this ModelBuilder builder)
    {
        // Profiles Context
        builder.Entity<Hotel>().HasKey(p => p.Id);
        builder.Entity<Hotel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Hotel>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Hotel>().Property(p => p.Images).IsRequired().HasMaxLength(100);
        builder.Entity<Hotel>().Property(p => p.Address).IsRequired().HasMaxLength(100);
        builder.Entity<Hotel>().Property(p => p.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<Hotel>().Property(p => p.ProfileId).IsRequired();
        builder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade); 

    }
}