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
    }
}
 