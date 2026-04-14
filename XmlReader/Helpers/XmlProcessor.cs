using XmlReader.Data.Context;
using XmlReader.Entities;
using XmlReader.Enums;
using XmlReader.Interfaces;
using XmlReader.Parsers;

namespace XmlReader.Helpers
{
    public class XmlProcessor
    {
        public static XML Process (string xmlContent)
        {
            var type = XmlTypeDetector.Detect(xmlContent);

            switch (type)
            {
                case EnumNf.NFe:
                    return NFeParser.Parse (xmlContent); 
                case EnumNf.NFCe:
                    return NFCeParser.Parse (xmlContent); 
                case EnumNf.NFSe:
                    return NFSeParser.Parse (xmlContent); 
                case EnumNf.CFe:
                    return CFeParser.Parse (xmlContent); 
                case EnumNf.CTe:
                    return CTeParser.Parse (xmlContent); 
                default: throw new Exception($"Tipo de xml não reconhecido! {type}");
            }
        }
    }
}
