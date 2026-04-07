using System.Xml.Linq;
using XmlReader.Entities;
using XmlReader.Enums;

namespace XmlReader.Parsers
{
    public class NFCeParser 
    {
        private readonly XNamespace _ns = "http://www.portalfiscal.inf.br/nfe";

        public XML Parse (string xmlContent)
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

            XML xml = new XML();

            xml.Key = infProt.Element(_ns + "chNFe").Value;
            xml.XmlNumber = ide.Element(_ns + "nNF").Value;
            xml.EmissionDate = DateTime.TryParse(
                ide.Element(_ns + "dhEmi").Value,
                out DateTime date) ? date : DateTime.MinValue;
            xml.IssuerDocument = emit.Element(_ns + "CNPJ").Value
                          ?? emit.Element(_ns + "CPF").Value;
            xml.SocialReasonIssuer = emit.Element(_ns + "xNome").Value;

            xml.Type = Enum.TryParse(
                ide.Element(_ns + "mod").Value,
                out EnumNf type) ? type : EnumNf.NFCe;

            return xml;
        }
    }
}
