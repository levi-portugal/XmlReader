using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using XmlReader.Dtos;
using XmlReader.Enums;
using XmlReader.Parsers.ParserInterface;

namespace XmlReader.Parsers
{
    public class CTeParser : IParser
    {
        private readonly XNamespace _ns = "http://www.portalfiscal.inf.br/cte";

        public XmlDto Parse(string xmlContent) 
        {
            XDocument doc = XDocument.Parse(xmlContent);

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

            return new XmlDto
            {
                Key = infProt.Element(_ns + "chCTe").Value,
                XmlNumber = ide.Element(_ns + "nCT").Value,

                EmissionDate = DateTime.TryParse(
                    ide.Element(_ns + "dhEmi").Value,
                    out DateTime date) ? date : DateTime.MinValue,

                IssuerDocument = emit.Element(_ns + "CNPJ").Value
                              ?? emit.Element(_ns + "CPF").Value,

                SocialReasonIssuer = emit.Element(_ns + "xNome").Value,

                RecipientDocument = dest?.Element(_ns + "CNPJ")?.Value
                                 ?? dest?.Element(_ns + "CPF")?.Value,

                ServiceTakerCnpj = toma4?.Element(_ns + "CNPJ")?.Value,

                ShipperCnpj = rem?.Element(_ns + "CNPJ")?.Value,

                Type = Enum.TryParse(
                    ide.Element(_ns + "mod").Value,
                    out EnumNf type) ? type : EnumNf.CTe
            };
        }
    }
}
