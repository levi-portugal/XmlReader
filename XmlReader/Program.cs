using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XmlReader.Data.Context;
using XmlReader.Data.Repositories;
using XmlReader.Entities;
using XmlReader.Helpers;
using XmlReader.Interfaces;
using XmlReader.Services;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string pastaXml = config["FileSettings:XmlFolderPath"];

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>();
services.AddScoped<IRepository<XML>, Repository<XML>>();
services.AddScoped<IRepository<FileTable>, Repository<FileTable>>();
services.AddScoped<IXmlService, XmlService>();
services.AddScoped<XmlFileReader>();

var serviceProvider = services.BuildServiceProvider(); 

// Aplica migrations usando o provider, não uma instância avulsa
using (var scope = serviceProvider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Resolve o XmlFileReader pelo container 
using (var scope = serviceProvider.CreateScope())
{
    var reader = scope.ServiceProvider.GetRequiredService<XmlFileReader>();
    reader.ProcessAndSaveFolder(pastaXml);
}

Console.WriteLine("Processamento finalizado. Pressione qualquer tecla para sair.");
Console.ReadKey();