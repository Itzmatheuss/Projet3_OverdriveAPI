using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto3_Over.Models;

namespace Projeto3_Over.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CPF).IsRequired().HasMaxLength(11);
            builder.HasIndex(u => u.CPF).IsUnique();
            builder.Property(u => u.Telefone).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Status).IsRequired().HasConversion<int>();
            builder.Property(u => u.EmpresaId).IsRequired(false);
            builder.HasOne(u => u.Empresa).WithMany().HasForeignKey(u => u.EmpresaId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
