using System.Reflection.Metadata;
using System.Xml.Linq;
using XmlReader.Data.Context;
using XmlReader.Entities;
using XmlReader.Enums;
using XmlReader.Helpers;

namespace XmlReader.Parsers
{
    public class NFeParser
    {
        //Contem o namespace do XML, sem isso ele vai procurar e retornar null toda vez, eu ia quebrar minha cabeça com isso
        private readonly XNamespace _ns = "http://www.portalfiscal.inf.br/nfe";

        public XML Parse(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            if (doc == null)
            {
                throw new Exception($"O documento veio Nulo!! veja o xml");
            }

            var infNFe = doc.Root
                            .Element(_ns + "NFe")
                            .Element(_ns + "infNFe");

            var ide = infNFe.Element(_ns + "ide"); //smp vai ter
            var emit = infNFe.Element(_ns + "emit"); // smp vai ter
            var dest = infNFe.Element(_ns + "dest");

            var infProt = doc.Root //smp vai ter por ser obrigatorio
                 .Element(_ns + "protNFe")
                 .Element(_ns + "infProt");

            XML xml = new XML();

            xml.Key = infProt.Element(_ns + "chNFe").Value;
            xml.XmlNumber = ide.Element(_ns + "nNF").Value;

            var dataRaw = ide.Element(_ns + "dEmi")?.Value
           ?? ide.Element(_ns + "dhEmi")?.Value;

            xml.EmissionDate = DateTime.TryParse(dataRaw, out DateTime date)
                ? date
                : DateTime.MinValue;
            // Vai tentar pegar ide, se n consegui, tudo nullo => vai tentar pegar o dhEmi
            // se n conseguir, null, vai tentar transformar isso em Datetime, se n conseguir, assume Minvalue.

            xml.IssuerDocument = emit.Element(_ns + "CNPJ").Value
                          ?? emit.Element(_ns + "CPF").Value;
            xml.SocialReasonIssuer = emit.Element(_ns + "xNome").Value;
            xml.RecipientDocument = dest?.Element(_ns + "CNPJ")?.Value;
            xml.RecipientName = dest?.Element(_ns + "xNome")?.Value;
            xml.SocialReasonRecipient = dest?.Element(_ns + "xNome")?.Value;
            xml.Type = Enum.TryParse(ide.Element(_ns + "mod").Value,
                    out EnumNf type) ? type : EnumNf.NFe;

            return xml;
        }
    }
}
