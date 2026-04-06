using XmlReader.Entities;

namespace XmlReader.Helpers
{
    public class XmlFileReader
    {
        public List<XML> ReadFolder (string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.xml");
            XmlProcessor processor = new XmlProcessor();
            
            var results = new List<XML>();
            foreach (var file in files)
            {
                try
                {
                    string content = File.ReadAllText(file);
                    var dto = processor.Process(content);
                    results.Add(dto);
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Erro ao processar {file}: {ex.Message}");
                }
                
            }

            return results;           
        }
    }
}
