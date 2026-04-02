using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using XmlReader.Enums;

namespace XmlReader.Entities
{
    public abstract class XML
    {
        public string Id { get; set; }//banco
        public string Key { get; set; }
        public string XmlNumeber { get; set; }
        public DateTime EmissionDate { get; set; }
        public string IssuerCnpj { get; set; }
        public string SocialReasonIssuer { get; set; }
        public EnumNf Type { get; set; }
    }
}
