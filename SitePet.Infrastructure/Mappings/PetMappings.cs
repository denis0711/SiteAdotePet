using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SitePet.Domain.Models;

namespace SitePet.Infrastructure.Mappings
{
    public class PetMappings : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(20)");
            builder.Property(p => p.Raca)
                .IsRequired()
                .HasColumnType("varchar(30)");
            builder.Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("varchar(100)");
            

            builder.ToTable("Pets");
        }
    }
}
