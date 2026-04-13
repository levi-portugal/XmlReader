using XmlReader.Data.Repositories;
using XmlReader.Entities;
using XmlReader.Helpers;
using XmlReader.Interfaces;

namespace XmlReader.Services
{
    public class XmlService : IXmlService
    {
        private readonly IRepository<FileTable> _repositoryFileTable;
        private readonly IRepository<XML> _repositoryXml;

        public XmlService(IRepository<FileTable> repositoryFileTable, IRepository<XML> repositoryXml)
        {
            _repositoryFileTable = repositoryFileTable;
            _repositoryXml = repositoryXml;
        }

        public void CreateXml(XML xml)
        {
            _repositoryXml.Create(xml);
        }
        public void CreateFileTable(FileTable fileTable)
        {
            _repositoryFileTable.Create(fileTable);
        }

        public string GetXmlById(string id)
        {
            var xml = _repositoryFileTable.GetById(id);
            if (xml != null)
            {
                return Base64Transform.ConvertBase64ToString(xml.Content.ToString());
            }
            else
            {
                throw new Exception("Nao foi possivel encontrar um Xml com esse Id");
            }
        }

        public void CreateXmlUsingBase64(string content)
        {
            var xml = Base64Transform.ConvertBase64ToString(content);

            var x = XmlProcessor.Process(xml);

            _repositoryXml.Create(x);
        }

        public List<XML> FilterXmlByProperties(string? issuerDocument, string? recipientDocument, string? shipperCnpj, DateTime? startDate, DateTime? endDate, string? serviceTakerCnpj)
        {
            var query = _repositoryXml.GetAll();

            if (issuerDocument != null)
            {
                query = query.Where(x => x.IssuerDocument == issuerDocument);
            }
            if (recipientDocument != null)
            {
                query = query.Where(x => x.RecipientDocument == recipientDocument);
            }
            if (shipperCnpj != null)
            {
                query = query.Where(x => x.ShipperCnpj == shipperCnpj);
            }
            if (serviceTakerCnpj != null)
            {
                query = query.Where(x => x.ServiceTakerCnpj == serviceTakerCnpj);
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(x => x.EmissionDate >= startDate && x.EmissionDate <= endDate);
            }
            else if (startDate.HasValue)
            {
                query = query.Where(x => x.EmissionDate >= startDate);
            }
            else if (endDate.HasValue)
            {
                query = query.Where(x => x.EmissionDate <= endDate);
            }
           
            return query.ToList();
        }
    }
}
