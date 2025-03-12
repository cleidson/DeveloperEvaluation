using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Relacionamento: O usuário pode ser um Cliente (Customer) que faz compras
        builder
            .HasMany(u => u.Purchases)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento: O usuário pode ser um Admin/Manager que registra vendas
        builder
            .HasMany(u => u.RegisteredSales)
            .WithOne(s => s.RegisteredByUser)
            .HasForeignKey(s => s.RegisteredByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(u => u.CreatedAt)
        .IsRequired()
         .HasConversion(
                       v => v.ToUniversalTime(),     // Ao salvar, converte para UTC
                       v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Ao ler, define como UTC
                   );

        builder.Property(u => u.UpdatedAt)
      .HasConversion(
          v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,  // Se não for nulo, converte para UTC ao salvar
          v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null // Se não for nulo, define UTC ao ler
      );

    }
}
