using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Dtos;
using XmlReader.Enums;
using XmlReader.Parsers;
using XmlReader.Parsers.ParserInterface;

namespace XmlReader.Helpers
{
    public class XmlProcessor
    {    
        public XmlDto Process (string xmlContent)
        {
           var type = XmlTypeDetector.Detect (xmlContent);

            switch (type)
            {
                case EnumNf.NFe:
                    return new NFeParser().Parse (xmlContent); // fazer com injecao
                case EnumNf.NFCe:
                    return new NFCeParser().Parse (xmlContent); //fazer com injecao
                case EnumNf.NFSe:
                    return new NFSeParser().Parse (xmlContent); // fazer com injecao
                case EnumNf.CFe:
                    return new CFeParser().Parse (xmlContent); // fazer com injecao
                case EnumNf.CTe:
                    return new CTeParser().Parse (xmlContent); // fazer com injecao
                default: throw new Exception($"Tipo de xml não reconhecido! {type}");
            }
        }
    }
}
