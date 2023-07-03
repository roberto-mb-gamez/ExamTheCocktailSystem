using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Persistence.SQLite.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");
            builder.HasKey(f => f.FavoriteId);
            builder.Property(f => f.FavoriteId).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(f => f.DrinkId)
                .IsRequired()
                .HasColumnType("NVARCHAR");
            builder.Property(f => f.DrinkImage)
                .IsRequired()
                .HasColumnType("NVARCHAR");
            builder.Property(f => f.DrinkName)
                .IsRequired()
                .HasColumnType("NVARCHAR");
        }
    }
}
