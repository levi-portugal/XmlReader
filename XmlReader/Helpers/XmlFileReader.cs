using XmlReader.Data.Context;
using XmlReader.Entities;
using XmlReader.Interfaces;
using XmlReader.Services;

namespace XmlReader.Helpers
{
    public class XmlFileReader
    {
        private readonly IXmlService _xmlService;

        // Recebe o contexto por injeção — quem cria o XmlFileReader decide o ciclo de vida do db
        public XmlFileReader(IXmlService xmlService)
        {
            _xmlService = xmlService;
        }

        public void ProcessAndSaveFolder(string folderPath)
        {
            //Mensagem se caso o programa nao encontrar a pasta
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Erro: A pasta '{folderPath}' não foi encontrada.");
                return;
            }

            //Array de arquivos que achar dentro da pasta 
            string[] files = Directory.GetFiles(folderPath, "*.xml");
            Console.WriteLine($"Encontrados {files.Length} arquivos para processar.");

            //vai pegar um por um
            foreach (var file in files)
            {

                //tenta processar 
                try
                {
                    Console.WriteLine($"Processando: {Path.GetFileName(file)}");

                    //pega todo o conteudo de cada arquivo que encontrar na pasta 
                    string content = File.ReadAllText(file);

                    //manda processar esse conteudo 
                    XML xml = XmlProcessor.Process(content);

                    FileTable fileTable = new FileTable()
                    {
                        FileKey = xml.Key,
                        Content = Base64Transform.ConvertToBase64(content)
                    };

                    //vai pegar o objeto xml que retornou do parser e vai tentar adicionar ao dbContext 
                    _xmlService.CreateXml(xml);
                    _xmlService.CreateFileTable(fileTable);

                    Console.WriteLine($"Sucesso: Nota {xml.XmlNumber} salva!");
                }
                catch (Exception ex)
                {
                    //vai cair aqui caso de algo de errado 
                    Console.WriteLine($"Erro ao processar {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }
    }
}
