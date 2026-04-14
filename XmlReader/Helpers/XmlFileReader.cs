using XmlReader.Data.Context;
using XmlReader.Entities;
using XmlReader.Interfaces;
using XmlReader.Services;

namespace XmlReader.Helpers
{
    public class XmlFileReader
    {
        private readonly IXmlService _xmlService;
        public XmlFileReader(IXmlService xmlService)
        {
            _xmlService = xmlService;
        }

        public void ProcessAndSaveFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Erro: A pasta '{folderPath}' não foi encontrada.");
                return;
            }
 
            string[] files = Directory.GetFiles(folderPath, "*.xml");
            Console.WriteLine($"Encontrados {files.Length} arquivos para processar.");

            foreach (var file in files)
            {
                try
                {
                    Console.WriteLine($"Processando: {Path.GetFileName(file)}");

                    string content = File.ReadAllText(file);

                    XML xml = XmlProcessor.Process(content);

                    FileTable fileTable = new FileTable()
                    {
                        Key = xml.Key,
                        Content = Base64Transform.ConvertToBase64(content)
                    };

                    _xmlService.CreateXml(xml);
                    _xmlService.CreateFileTable(fileTable);

                    Console.WriteLine($"Sucesso: Nota {xml.XmlNumber} salva!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }
    }
}
