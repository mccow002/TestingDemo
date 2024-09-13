using Library.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configs;

public class CheckoutConfig : IEntityTypeConfiguration<Checkout>
{
    public void Configure(EntityTypeBuilder<Checkout> builder)
    {
        builder.Property(x => x.CheckoutId)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Fine)
            .WithOne(x => x.Checkout)
            .HasForeignKey<Fine>(x => x.CheckoutId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}