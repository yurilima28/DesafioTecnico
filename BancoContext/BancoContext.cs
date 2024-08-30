using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Dapper
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) 
        {
        }
        public DbSet<FabricantesModel> Fabricantes { get; set; }
        public DbSet<VeiculosModel> Veiculos { get; set; }
        public DbSet<ConcessionariasModel> Concessionarias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VeiculosModel>()
                .HasOne(v => v.Fabricantes)
                .WithMany(f => f.Veiculos)
                .HasForeignKey(v => v.FabricanteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConcessionariasModel>()
                .HasNoKey();
        }
    }
}
 