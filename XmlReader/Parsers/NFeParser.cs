using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using XmlReader.Dtos;
using XmlReader.Entities;
using XmlReader.Enums;

namespace XmlReader.Parsers
{
    public class NFeParser
    {
        //Contem o namespace do XML, sem isso ele vai procurar e retornar null toda vez, eu ia quebrar minha cabeça com isso
        private readonly XNamespace _ns = "http://www.portalfiscal.inf.br/nfe";

        public XmlDto Parse(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            var infNFe = doc.Root
                            .Element(_ns + "NFe")
                            .Element(_ns + "infNFe");

            var ide = infNFe.Element(_ns + "ide"); //smp vai ter
            var emit = infNFe.Element(_ns + "emit"); // smp vai ter
            var dest = infNFe.Element(_ns + "dest");

            var infProt = doc.Root //smp vai ter por ser obrigatorio
                 .Element(_ns + "protNFe")
                 .Element(_ns + "infProt");

            return new XmlDto 
            {
                Key = infProt.Element(_ns + "chNFe").Value,
                XmlNumber = ide.Element(_ns + "nNF").Value,

                EmissionDate = DateTime.TryParse(
                    ide.Element(_ns + "dhEmi").Value,
                    out DateTime date) ? date : DateTime.MinValue,
                // Vai tentar pegar ide, se n consegui, tudo nullo => vai tentar pegar o dhEmi
                // se n conseguir, null, vai tentar transformar isso em Datetime, se n conseguir, assume Minvalue.

                IssuerDocument = emit.Element(_ns + "CNPJ").Value
                              ?? emit.Element(_ns + "CPF").Value,
                SocialReasonIssuer = emit.Element(_ns + "xNome").Value,
                RecipientDocument = dest?.Element(_ns + "CNPJ")?.Value,
                SocialReasonRecipient = dest?.Element(_ns + "xNome")?.Value,
                Type = Enum.TryParse(ide.Element(_ns + "mod").Value,
                        out EnumNf type) ? type : EnumNf.NFe,
            };
        }
    }
}
