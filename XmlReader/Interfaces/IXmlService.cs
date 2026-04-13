using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Entities;

namespace XmlReader.Interfaces
{
     public interface IXmlService
     {
         public void CreateXml(XML xml);
         public void CreateFileTable(FileTable fileTable);
         public string GetXmlById(string id);
         public void CreateXmlUsingBase64(string content);
         public List<XML> FilterXmlByProperties(string? issuerDocument, string? recipientDocument, string? shipperCnpj, DateTime? startDate, DateTime? endDate, string? serviceTakerCnpj);
     }
}
