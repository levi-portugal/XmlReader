using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Dtos;
using XmlReader.Entities;

namespace XmlReader.Parsers.ParserInterface
{
    public interface IParser
    {
        public XML Parse(string xmlContent);
    }
}
