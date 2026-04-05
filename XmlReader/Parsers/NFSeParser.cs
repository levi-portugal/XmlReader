using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using XmlReader.Dtos;
using XmlReader.Parsers.ParserInterface;

namespace XmlReader.Parsers
{
    public class NFSeParser : IParser
    {
        private readonly XNamespace _ns = "http://www.abrasf.org.br/nfse.xsd";

        public XmlDto Parse(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            var infNfse = doc.Root
                             .Element(_ns + "ListaNfse")
                             .Element(_ns + "CompNfse")
                             .Element(_ns + "Nfse")
                             .Element(_ns + "InfNfse");

            var documentIssuer = infNfse.Element(_ns + "DeclaracaoPrestacaoServico")
                                        .Element(_ns + "InfDeclaracaoPrestacaoServico")
                                        .Element(_ns + "Prestador")
                                        .Element(_ns + "CpfCnpj");

            var prestadorServico = infNfse.Element(_ns + "PrestadorServico");

            var documentRecipient = infNfse.Element(_ns + "DeclaracaoPrestacaoServico")
                                           .Element(_ns + "InfDeclaracaoPrestacaoServico")
                                           .Element(_ns + "TomadorServico")
                                           .Element(_ns + "IdentificacaoTomador")
                                           .Element(_ns + "CpfCnpj");

            return new XmlDto
            {
                Key = infNfse.Element(_ns + "CodigoVerificacao").Value,
                XmlNumber = infNfse.Element(_ns + "Numero").Value,
                EmissionDate = DateTime.TryParse(
                    infNfse.Element(_ns + "DataEmissao").Value,
                    out DateTime date) ? date : DateTime.MinValue,

                IssuerDocument = documentIssuer?.Element(_ns + "Cnpj")?.Value
                              ?? documentIssuer?.Element(_ns + "Cpf")?.Value,

                SocialReasonIssuer = prestadorServico.Element(_ns + "RazaoSocial").Value,

                RecipientDocument = documentRecipient?.Element(_ns + "Cnpj")?.Value
                                 ?? documentRecipient?.Element(_ns + "Cpf")?.Value,

                Type = XmlReader.Enums.EnumNf.NFSe
            };
        }
    }
}
