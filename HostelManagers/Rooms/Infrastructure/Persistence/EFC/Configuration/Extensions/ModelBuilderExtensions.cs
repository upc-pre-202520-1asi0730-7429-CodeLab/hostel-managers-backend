using HostelManagers.Rooms.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Rooms.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyRoomsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Room>().HasKey(p => p.Id);
        builder.Entity<Room>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(p => p.Type).IsRequired(); 
        builder.Entity<Room>().Property(p => p.Price).IsRequired();
    }
}