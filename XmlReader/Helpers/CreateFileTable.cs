using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Entities;
using XmlReader.Interfaces;

namespace XmlReader.Helpers
{
    public class CreateFileTable
    {
        public static IXmlService _xmlService { get; set; }

        public CreateFileTable(IXmlService xmlService)
        {
            _xmlService = xmlService;
        }

        public static FileTable Create(XML xml, string xmlContent)
        {
            FileTable file = new FileTable();

            file.Key = xml.Key;
            file.Content = Base64Transform.ConvertToBase64(xmlContent);

            _xmlService.CreateFileTable(file);

            return file;
        }
    }
}
