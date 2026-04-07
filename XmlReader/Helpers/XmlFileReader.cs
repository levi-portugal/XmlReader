using XmlReader.Data.Context;
using XmlReader.Entities;

namespace XmlReader.Helpers
{
    public class XmlFileReader
    {
        private readonly AppDbContext _db;
        private readonly XmlProcessor _processor;

        // Recebe o contexto por injeção — quem cria o XmlFileReader decide o ciclo de vida do db
        public XmlFileReader(AppDbContext db)
        {
            _db = db;
            _processor = new XmlProcessor();
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
                    XML xml = _processor.Process(content);

                    //vai pegar o objeto xml que retornou do parser e vai tentar adicionar ao dbContext 
                    _db.Xmls.Add(xml);
                    _db.SaveChanges();

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
