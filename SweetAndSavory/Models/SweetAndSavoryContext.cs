using Microsoft.EntityFrameworkCore;

namespace SweetAndSavory.Models
{
    public class SweetAndSavoryContext : DbContext
    {
        public virtual DbSet<Flavor> Flavors { get; set; }
        public DbSet<Treats> Treatss { get; set; }
        public DbSet<FlavorTreats> FlavorTreats { get; set; }

        public SweetAndSavoryContext(DbContextOptions options) : base(options) { }
    }
}