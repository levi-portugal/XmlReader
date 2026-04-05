using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using XmlReader.Dtos;
using XmlReader.Enums;
using XmlReader.Parsers.ParserInterface;

namespace XmlReader.Parsers
{
    public class NFCeParser : IParser
    {
        private readonly XNamespace _ns = "http://www.portalfiscal.inf.br/nfe";

        public XmlDto Parse (string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            var infNFe = doc.Root
                            .Element(_ns + "NFe")
                            .Element(_ns + "infNFe");
            var infProt = doc.Root
                            .Element(_ns + "protNFe")
                            .Element(_ns + "infProt");
            var ide = infNFe.Element(_ns + "ide");

            var emit = infNFe.Element(_ns + "emit");
            return new XmlDto 
            {
                 Key = infProt.Element(_ns + "chNFe").Value,
                 XmlNumber = ide.Element(_ns + "nNF").Value,
                 EmissionDate = DateTime.TryParse(
                     ide.Element(_ns + "dhEmi").Value,
                     out DateTime date) ? date : DateTime.MinValue,
                 IssuerDocument = emit.Element(_ns + "CNPJ").Value
                               ?? emit.Element(_ns + "CPF").Value,
                 SocialReasonIssuer = emit.Element(_ns + "xNome").Value,
                 
                 Type = Enum.TryParse(
                     ide.Element(_ns + "mod").Value,
                     out EnumNf type) ? type : EnumNf.NFCe
            };
        }
    }
}
