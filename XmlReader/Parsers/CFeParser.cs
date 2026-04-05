using System.Globalization;
using System.Xml.Linq;
using XmlReader.Dtos;
using XmlReader.Parsers.ParserInterface;

namespace XmlReader.Parsers
{
    public class CFeParser : IParser
    {
        public XmlDto Parse (string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            //smp
            var infCFe = doc.Root
                            .Element("infCFe");

            var ide = infCFe.Element("ide");

            var emit = infCFe.Element("emit");

            var dest = infCFe.Element("dest");

            return new XmlDto
            {
                Key = infCFe.Attribute("Id").Value,
                XmlNumber = ide.Element("nCFe").Value,

                EmissionDate = DateTime.TryParseExact(
                    ide.Element("dEmi").Value + ide.Element("hEmi").Value,
                    "yyyyMMddHHmmss",
                    System.Globalization.CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date
                    ) ? date : DateTime.MinValue,

                IssuerDocument = emit.Element("CNPJ").Value
                              ?? emit.Element("CPF").Value,

                SocialReasonIssuer = emit.Element("xNome").Value,

                RecipientDocument = dest?.Element("CNPJ")?.Value
                                 ?? dest?.Element("CPF")?.Value,

                Type = XmlReader.Enums.EnumNf.CFe
            };
        }
    }
}
