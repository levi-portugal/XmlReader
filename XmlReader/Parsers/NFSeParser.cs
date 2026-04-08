using System.Xml.Linq;
using XmlReader.Entities;

namespace XmlReader.Parsers
{
    public class NFSeParser 
    {
        private readonly XNamespace _ns = "http://www.sped.fazenda.gov.br/nfse";

        public XML Parse(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            var infNFSe = doc.Root
                             .Element(_ns + "infNFSe");

            var emit = infNFSe.Element(_ns + "emit");

            var toma = doc.Root
                           .Element(_ns + "infNFSe")
                           .Element(_ns + "DPS")
                           .Element(_ns + "infDPS")
                           .Element(_ns + "toma");

            XML xml = new XML();

            xml.Key = infNFSe.Attribute("Id").Value;

            xml.XmlNumber = infNFSe.Element(_ns + "nNFSe").Value;

            xml.EmissionDate = DateTime.TryParse(
                infNFSe.Element(_ns + "dhProc").Value,
                out DateTime date) ? date : DateTime.MinValue;

            xml.IssuerDocument = emit?.Element(_ns + "CNPJ")?.Value
                          ?? emit?.Element(_ns + "CPF")?.Value;
                        
            xml.SocialReasonIssuer = emit.Element(_ns + "xNome").Value;

            xml.RecipientDocument = toma?.Element(_ns + "CNPJ")?.Value
                             ?? toma?.Element(_ns + "CPF")?.Value;

            xml.Type = XmlReader.Enums.EnumNf.NFSe;

            return xml;
        }
    }
}
