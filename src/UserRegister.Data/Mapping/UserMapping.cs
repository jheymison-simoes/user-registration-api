using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRegister.Business.EntityModels;

namespace UserRegister.Data.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .HasColumnOrder(0);
        builder.Property(b => b.Code)
            .HasColumnName("code")
            .HasDefaultValueSql("nextval('\"UserSequence\"')")
            .HasColumnOrder(1);
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnOrder(2);
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name")
            .HasColumnOrder(3);
        builder.Property(b => b.LegalPerson)
            .HasColumnName("legal_person")
            .HasColumnOrder(4);
        builder.Property(b => b.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnName("cpf")
            .HasColumnOrder(5);
        builder.Property(b => b.Cnpj)
            .IsRequired(false)
            .HasMaxLength(14)
            .HasColumnName("cnpj")
            .HasColumnOrder(6);
        builder.Property(b => b.CorporateName)
            .IsRequired(false)
            .HasMaxLength(14)
            .HasColumnName("corporateName")
            .HasColumnOrder(7);
        builder.Property(b => b.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasColumnOrder(8);
        builder.Property(b => b.AddressId)
            .HasColumnName("address_id")
            .HasColumnOrder(9);

        #region RelacionShip
        builder.HasOne(b => b.Address)
            .WithOne(b => b.User)
            .HasForeignKey<User>(b => b.AddressId)
            .HasConstraintName("fk_user_addressId");
        #endregion

    }
}