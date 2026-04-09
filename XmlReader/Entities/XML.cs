using XmlReader.Enums;

namespace XmlReader.Entities
{
    public class XML
    {
        public int Id { get; set; }//banco
        public string Key { get; set; }
        public string XmlNumber { get; set; }
        public DateTime EmissionDate { get; set; }
        public string IssuerDocument { get; set; }
        public string SocialReasonIssuer { get; set; }
        public string? RecipientDocument { get; set; }        // null pra NFCe e NFSe
        public string? SocialReasonRecipient { get; set; } // null pra maioria
        public EnumNf Type { get; set; }
        public string? ServiceTakerCnpj { get; set; }
        public string? ShipperCnpj { get; set; }
        public string? RecipientName { get; set; }
    }
}
