using XmlReader.Data.Repository;
using XmlReader.Helpers;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection");
string folderPath = config["Settings:XmlFolderPath"];

var repository = new XmlRepository(connectionString);
var reader = new XmlFileReader();

var dtos = reader.ReadFolder(folderPath);

foreach (var dto in dtos)
{
    repository.SaveInSql(dto);
    Console.WriteLine($"Salvo: {dto.Type} - {dto.XmlNumber}");
}

Console.WriteLine($"\nTotal salvo: {dtos.Count}");

foreach (var dto in dtos)
{
    Console.WriteLine("----------------------------");
    Console.WriteLine($"Tipo: {dto.Type}");
    Console.WriteLine($"Chave: {dto.Key}");
    Console.WriteLine($"Número: {dto.XmlNumber}");
    Console.WriteLine($"Emissão: {dto.EmissionDate}");
    Console.WriteLine($"CNPJ Emitente: {dto.IssuerDocument}");
    Console.WriteLine($"Razão Emitente: {dto.SocialReasonIssuer}");
    Console.WriteLine($"Documento Destinatário: {dto.RecipientDocument}");
    Console.WriteLine($"Razão Destinatário: {dto.SocialReasonRecipient}");
    Console.WriteLine($"Tomador: {dto.ServiceTakerCnpj}");
    Console.WriteLine($"Remetente: {dto.ShipperCnpj}");
}

Console.WriteLine($"\nTotal processados: {dtos.Count}");