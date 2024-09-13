using Library.Domain.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Configs;

public class FineConfig : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.Property(x => x.FineId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Amount)
            .HasColumnType("money");
    }
}