using System.Xml.Linq;
using XmlReader.Entities;
using XmlReader.Enums;
using XmlReader.Helpers;

namespace XmlReader.Parsers
{
    public class CTeParser
    {
        private static readonly XNamespace _ns = "http://www.portalfiscal.inf.br/cte";

        public static XML Parse(string xmlContent) 
        {
            XDocument doc = XDocument.Parse(xmlContent);

            if (doc == null)
            {
                throw new Exception($"O documento veio Nulo!! veja o xml");
            }

            var infCte = doc.Root
                            .Element(_ns + "CTe")
                            .Element(_ns + "infCte");
            var ide = infCte.Element(_ns + "ide");
            var emit = infCte.Element(_ns + "emit");
            var infProt = doc.Root
                             .Element(_ns + "protCTe")
                             .Element(_ns + "infProt");
            var dest = infCte.Element(_ns + "dest");
            var toma4 = ide.Element(_ns + "toma4");
            var rem = infCte.Element(_ns + "rem");

            XML xml = new XML();

            xml.Key = infProt.Element(_ns + "chCTe").Value;
            xml.XmlNumber = ide.Element(_ns + "nCT").Value;
            xml.EmissionDate = DateTime.TryParse(
                ide.Element(_ns + "dhEmi").Value,
                out DateTime date) ? date : DateTime.MinValue;
            xml.IssuerDocument = emit.Element(_ns + "CNPJ").Value
                          ?? emit.Element(_ns + "CPF").Value;
            xml.SocialReasonIssuer = emit.Element(_ns + "xNome").Value;
            xml.RecipientDocument = dest?.Element(_ns + "CNPJ")?.Value
                             ?? dest?.Element(_ns + "CPF")?.Value;
            xml.ServiceTakerCnpj = toma4?.Element(_ns + "CNPJ")?.Value;
            xml.ShipperCnpj = rem?.Element(_ns + "CNPJ")?.Value;
            xml.RecipientName = toma4?.Element(_ns + "xNome")?.Value;
            xml.Type = Enum.TryParse(
                ide.Element(_ns + "mod").Value,
                out EnumNf type) ? type : EnumNf.CTe;

            return xml;
        }
    }
}
