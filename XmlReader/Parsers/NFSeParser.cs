using System.Xml.Linq;
using XmlReader.Entities;
using XmlReader.Helpers;

namespace XmlReader.Parsers
{
    public class NFSeParser 
    {
        private static readonly XNamespace _ns = "http://www.sped.fazenda.gov.br/nfse";

        public static XML Parse(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            if (doc == null)
            {
                throw new Exception($"O documento veio Nulo!! veja o xml");
            }

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
            xml.RecipientName = toma?.Element(_ns + "xNome")?.Value;

            return xml;
        }
    }
}
