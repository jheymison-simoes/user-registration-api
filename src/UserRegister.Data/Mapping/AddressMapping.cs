using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRegister.Business.EntityModels;

namespace UserRegister.Data.Mapping;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .HasColumnOrder(0);
        builder.Property(b => b.Code)
            .HasColumnName("code")
            .HasDefaultValueSql("nextval('\"AddressSequence\"')")
            .HasColumnOrder(1);
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnOrder(2);
        builder.Property(b => b.Street)
            .IsRequired()
            .HasColumnName("street")
            .HasColumnOrder(3);
        builder.Property(b => b.PostalCode)
            .IsRequired()
            .HasColumnName("postal_code")
            .HasColumnOrder(4);
        builder.Property(b => b.District)
            .IsRequired()
            .HasColumnName("district")
            .HasColumnOrder(5);
        builder.Property(b => b.City)
            .IsRequired()
            .HasColumnName("city")
            .HasColumnOrder(6);
        builder.Property(b => b.State)
            .IsRequired()
            .HasColumnName("state")
            .HasColumnOrder(7);
    }
}