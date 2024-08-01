using Microsoft.EntityFrameworkCore;
using Projeto3_Over.Data.Map;
using Projeto3_Over.Models;

namespace Projeto3_Over.Data
{
    public class Projeto3DBContext : DbContext
    {
        public Projeto3DBContext(DbContextOptions<Projeto3DBContext> options) : base(options)
        {
        }

        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
