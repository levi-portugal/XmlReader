using System.Xml.Linq;
using XmlReader.Entities;

namespace XmlReader.Parsers
{
    public class NFSeParser 
    {
        private readonly XNamespace _ns = "http://www.abrasf.org.br/nfse.xsd";

        public XML Parse(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            var infNfse = doc.Root
                             ?.Element(_ns + "ListaNfse")
                             ?.Element(_ns + "CompNfse")
                             ?.Element(_ns + "Nfse")
                             ?.Element(_ns + "InfNfse");

            var documentIssuer = infNfse.Element(_ns + "DeclaracaoPrestacaoServico")
                                        ?.Element(_ns + "InfDeclaracaoPrestacaoServico")
                                        ?.Element(_ns + "Prestador")
                                        ?.Element(_ns + "CpfCnpj");

            var prestadorServico = infNfse.Element(_ns + "PrestadorServico");

            var documentRecipient = infNfse.Element(_ns + "DeclaracaoPrestacaoServico")
                                           ?.Element(_ns + "InfDeclaracaoPrestacaoServico")
                                           ?.Element(_ns + "TomadorServico")
                                           ?.Element(_ns + "IdentificacaoTomador")
                                           ?.Element(_ns + "CpfCnpj");
         
            XML xml = new XML();

            xml.Key = infNfse.Element(_ns + "CodigoVerificacao").Value;
            xml.XmlNumber = infNfse.Element(_ns + "Numero").Value;
            xml.EmissionDate = DateTime.TryParse(
                infNfse.Element(_ns + "DataEmissao").Value,
                out DateTime date) ? date : DateTime.MinValue;

            xml.IssuerDocument = documentIssuer?.Element(_ns + "Cnpj")?.Value
                          ?? documentIssuer?.Element(_ns + "Cpf")?.Value;

            xml.SocialReasonIssuer = prestadorServico.Element(_ns + "RazaoSocial").Value;

            xml.RecipientDocument = documentRecipient?.Element(_ns + "Cnpj")?.Value
                             ?? documentRecipient?.Element(_ns + "Cpf")?.Value;

            xml.Type = XmlReader.Enums.EnumNf.NFSe;

            return xml;
        }
    }
}
