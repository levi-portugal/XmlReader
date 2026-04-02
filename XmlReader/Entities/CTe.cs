using System;
using System.Collections.Generic;
using System.Text;

namespace XmlReader.Entities
{
    public class CTe : XML
    {
        public string RecipientCnpj { get; set; } 
        public string SocialReasonIssuer { get; set; } 
        public string ServiceTakerCnpj { get; set; } 
        public string ShipperCnpj { get; set; } 
    }
}
