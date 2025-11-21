using HostelManagers.Suscriptions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace HostelManagers.Suscriptions.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySuscriptionsConfiguration(this ModelBuilder builder)
    {
        // Profiles Context
        builder.Entity<Suscription>().HasKey(p => p.Id);
        builder.Entity<Suscription>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Suscription>().Property(p => p.Plan).IsRequired();
        builder.Entity<Suscription>().Property(p => p.PayPalTransactionId).IsRequired();
        builder.Entity<Suscription>().Property(p => p.Statu).IsRequired();
        builder.Entity<Suscription>()
            .HasMany(s => s.Profiles)
            .WithOne(p => p.Suscription)
            .HasForeignKey(p => p.SuscriptionId);

    }
}