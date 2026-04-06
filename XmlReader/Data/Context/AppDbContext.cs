using Microsoft.EntityFrameworkCore;
using XmlReader.Entities;

namespace XmlReader.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<XML> XmlDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a Chave Primária pra n lascar o id 
            modelBuilder.Entity<XML>().HasKey(x => x.Id);

            // Mapeamentos de tamanho humilde
            modelBuilder.Entity<XML>().Property(x => x.Key).HasMaxLength(100);
            modelBuilder.Entity<XML>().Property(x => x.XmlNumber).HasMaxLength(20);
            modelBuilder.Entity<XML>().Property(x => x.IssuerDocument).HasMaxLength(20);
            modelBuilder.Entity<XML>().Property(x => x.RecipientDocument).HasMaxLength(20);

            base.OnModelCreating(modelBuilder);
        }

    }
}
