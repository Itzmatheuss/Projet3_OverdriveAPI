using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto3_Over.Models;

namespace Projeto3_Over.Data.Map
{
    public class EmpresaMap : IEntityTypeConfiguration<EmpresaModel>
    {
        public void Configure(EntityTypeBuilder<EmpresaModel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            builder.Property(e => e.NomeFantasia).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CNPJ).IsRequired().HasMaxLength(14);
            builder.Property(e => e.DataCadastro).IsRequired();
            builder.Property(e => e.Cnae).IsRequired().HasMaxLength(6);
            builder.Property(e => e.NaturezaJuridica).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Cep).IsRequired().HasMaxLength(8);
            builder.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Rua).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Numero).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Estado).IsRequired().HasMaxLength(2);
            builder.Property(e => e.Complemento).HasMaxLength(100);
            builder.Property(e => e.Telefone).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Capital).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Status).IsRequired().HasConversion<int>();
         

        }
    }
}
