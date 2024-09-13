using Library.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configs;

public class ReservationStatusConfig : IEntityTypeConfiguration<ReservationStatus>
{
    public void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
        builder.HasData(
            ReservationStatus.Reserved,
            ReservationStatus.Fulfilled
        );
    }
}