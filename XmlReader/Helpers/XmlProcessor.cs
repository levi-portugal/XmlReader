using XmlReader.Data.Context;
using XmlReader.Entities;
using XmlReader.Enums;
using XmlReader.Parsers;

namespace XmlReader.Helpers
{
    public class XmlProcessor
    {
        public XML Process (string xmlContent)
        {
           var type = XmlTypeDetector.Detect (xmlContent);

            switch (type)
            {
                case EnumNf.NFe:
                    return new NFeParser().Parse (xmlContent); 
                case EnumNf.NFCe:
                    return new NFCeParser().Parse (xmlContent); 
                case EnumNf.NFSe:
                    return new NFSeParser().Parse (xmlContent); 
                case EnumNf.CFe:
                    return new CFeParser().Parse (xmlContent); 
                case EnumNf.CTe:
                    return new CTeParser().Parse (xmlContent); 
                default: throw new Exception($"Tipo de xml não reconhecido! {type}");
            }
        }
    }
}
