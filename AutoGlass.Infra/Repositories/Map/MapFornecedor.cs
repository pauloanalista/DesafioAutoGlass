using AutoGlass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoGlass.Infra.Repositories.Map
{
    public class MapFornecedor : IEntityTypeConfiguration<Fornecedor>
    {

        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");

            ////Propriedades
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Codigo).HasMaxLength(36).IsRequired();
        }
    }
}
