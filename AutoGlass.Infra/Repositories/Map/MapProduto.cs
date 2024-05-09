using AutoGlass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoGlass.Infra.Repositories.Map
{
    public class MapProduto : IEntityTypeConfiguration<Produto>
    {

        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            ////Propriedades
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Codigo).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Descricao).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.DataFabricacao).IsRequired();
            builder.Property(x => x.DataValidade).IsRequired();
            builder.Property(x => x.Situacao).IsRequired();

            //Foreikey
            builder.HasOne(x => x.Fornecedor).WithMany().HasForeignKey("IdFornecedor");
        }
    }
}
