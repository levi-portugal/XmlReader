using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XmlReader.Data.Context;
using XmlReader.Helpers;

// isso aqui vai carregar o Appsetingjson e disponibiliza as configs pro programa
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

//le o caminho da pasta de xml das configs
string pastaXml = config["FileSettings:XmlFolderPath"];

// nova instancia do DbContext
using var db = new AppDbContext();

// Aplica todas as migrations pendentes automaticamente (ou cria o banco se não existir)
db.Database.Migrate();

// instancia o FileReader passando o bd 
var reader = new XmlFileReader(db); //injeçãozinha
//passa a pasta pra classe de processar e salvar no banco
reader.ProcessAndSaveFolder(pastaXml);

Console.WriteLine("Processamento finalizado. Pressione qualquer tecla para sair.");
Console.ReadKey();