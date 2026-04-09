using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Entities;

namespace XmlReader.Helpers
{
    public class CreateFileTable
    {
        public static FileTable Create(XML xml, string xmlContent)
        {
            FileTable file = new FileTable();

            file.FileKey = xml.Key;
            file.Content = Base64Transform.ConvertToBase64(xmlContent);
            return file;
        }
    }
}
