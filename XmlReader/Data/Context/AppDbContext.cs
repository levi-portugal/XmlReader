using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XmlReader.Entities;

namespace XmlReader.Data.Context
{
    public class AppDbContext : DbContext
    {
        //Isso vai dizer pro entity Framework pra criar uma tabela chamada Xmls baseado na entidade XML 
        public DbSet<XML> Xmls { get; set; }
        public DbSet<FileTable> FilesTable { get; set; }

        //Talvez o OnConfiguring nao seja a maneira correta quando se tiver DI corretamente 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //so vai fazer isso se n tiver configurado 
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString); //Define o SqlServer como provider, passa  aconnection string que buscou la no AppSetingsjson.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<XML>(entity =>
            {
                entity.HasKey(e => e.Id); // Define a Chave Primária

              //  entity.Property(e => e.Key).HasMaxLength(44); // Chave da NFe tem 44 caracteres
                entity.Property(e => e.IssuerDocument).HasMaxLength(14); // CNPJ

                // Campos opcionais — EF vai gerar como nullable no banco
                entity.Property(e => e.RecipientDocument).IsRequired(false);
                entity.Property(e => e.SocialReasonRecipient).IsRequired(false);
                entity.Property(e => e.ServiceTakerCnpj).IsRequired(false);
                entity.Property(e => e.ShipperCnpj).IsRequired(false);

                // Converte o Enum para Inteiro no banco (salva 55, 57, etc)
                entity.Property(e => e.Type).HasConversion<int>();
            });

            modelBuilder.Entity<FileTable>(entity =>
            {
                entity.HasKey(e => e.Key);
            });
        }
    }
}