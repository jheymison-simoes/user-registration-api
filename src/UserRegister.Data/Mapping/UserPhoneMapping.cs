using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRegister.Business.EntityModels;

namespace UserRegister.Data.Mapping;

public class UserPhoneMapping : IEntityTypeConfiguration<UserPhone>
{
    public void Configure(EntityTypeBuilder<UserPhone> builder)
    {
        builder.ToTable("user_phone");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .HasColumnOrder(0);
        builder.Property(b => b.Code)
            .HasColumnName("code")
            .HasDefaultValueSql("nextval('\"UserPhoneSequence\"')")
            .HasColumnOrder(1);
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnOrder(2);
        builder.Property(b => b.UserId)
            .HasColumnName("user_id")
            .HasColumnOrder(3);
        builder.Property(b => b.Ddd)
            .IsRequired()
            .HasColumnName("ddd")
            .HasColumnOrder(4);
        builder.Property(b => b.NumberPhone)
            .HasColumnName("number_phone")
            .HasColumnOrder(5);

        #region RelacionShip
        builder.HasOne(b => b.User)
            .WithMany(b => b.UserPhones)
            .HasForeignKey(b => b.UserId)
            .HasConstraintName("fk_user_phone_userId");
        #endregion
    }
}