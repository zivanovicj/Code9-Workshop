using Code9.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Code9.Infrastructure.EntityConfiguration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Region)
                .IsRequired();

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Cinema)
                 .WithOne(c => c.Cities)
                 .HasForeignKey(c => c.CityId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired();

            builder.HasIndex(c => new { c.Region }, "City_Regions_Ascending");
        }
    }
}
