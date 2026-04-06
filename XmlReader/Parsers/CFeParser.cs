using System.Globalization;
using System.Xml.Linq;
using XmlReader.Parsers.ParserInterface;
using XmlReader.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace XmlReader.Parsers
{
    public class CFeParser : IParser
    {
        public XML Parse (string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            //smp
            var infCFe = doc.Root
                            .Element("infCFe");

            var ide = infCFe.Element("ide");

            var emit = infCFe.Element("emit");

            var dest = infCFe.Element("dest");

            XML xml = new XML();


            xml.Key = infCFe.Attribute("Id").Value;
            xml.XmlNumber = ide.Element("nCFe").Value;

            xml.EmissionDate = DateTime.TryParseExact(
                ide.Element("dEmi").Value + ide.Element("hEmi").Value,
                "yyyyMMddHHmmss",
                System.Globalization.CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime date
                ) ? date : DateTime.MinValue;

            xml.IssuerDocument = emit.Element("CNPJ").Value
                          ?? emit.Element("CPF").Value;

            xml.SocialReasonIssuer = emit.Element("xNome").Value;

            xml.RecipientDocument = dest?.Element("CNPJ")?.Value
                             ?? dest?.Element("CPF")?.Value;

            xml.Type = XmlReader.Enums.EnumNf.CFe;

            return xml;
        }
    }
}