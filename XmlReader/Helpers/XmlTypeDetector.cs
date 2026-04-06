using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using XmlReader.Enums;

namespace XmlReader.Helpers
{
    public class XmlTypeDetector
    {
        public static EnumNf Detect(string xmlContent)
        {
            XDocument doc = XDocument.Parse(xmlContent);

            string rootTag = doc.Root.Name.LocalName;

            switch (rootTag)
            {
                case "nfeProc":
                    //Diferenciamento de Nfe e Nfce
                    XNamespace _ns = "http://www.portalfiscal.inf.br/nfe";
                    var mod = doc.Root
                                 .Element(_ns + "NFe")
                                 .Element(_ns + "infNFe")
                                 .Element(_ns + "ide")
                                 .Element(_ns + "mod")
                                 .Value;
                    return (EnumNf)int.Parse(mod);

                case "cteProc":
                    return EnumNf.CTe;

                case "CFe":
                    return EnumNf.CFe;

                case "ConsultarNfseServicoPrestadoResposta":
                    return EnumNf.NFSe;

                default:
                    throw new Exception($"Tipo de XMl não reconhecido: {rootTag}");
            }
        }
    }
}
