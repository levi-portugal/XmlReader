using XmlReader.Data.Repository;
using XmlReader.Helpers;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection");
string folderPath = config["Settings:XmlFolderPath"];

//var repository = new XmlRepository(connectionString);
var reader = new XmlFileReader();

var xmls = reader.ReadFolder(folderPath);

