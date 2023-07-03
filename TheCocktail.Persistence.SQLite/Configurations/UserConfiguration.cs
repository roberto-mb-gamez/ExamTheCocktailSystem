using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Persistence.SQLite.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .IsRequired();
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasColumnType("NVARCHAR");

            builder.HasMany(u => u.Favorites)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            // Test data (Para propositos del examen no se maneja una administración de usuarios y sesiones completa)
            builder.HasData(new User
            {
                UserId = 1,
                UserName = "TestCocktail"
            });
        }
    }
}
