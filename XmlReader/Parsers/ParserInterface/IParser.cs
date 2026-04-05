using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Dtos;

namespace XmlReader.Parsers.ParserInterface
{
    public interface IParser
    {
        public XmlDto Parse(string xmlContent);
    }
}
