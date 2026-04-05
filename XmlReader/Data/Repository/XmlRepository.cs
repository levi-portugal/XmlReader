using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Dtos;

namespace XmlReader.Data.Repository
{
    public class XmlRepository
    {
        private readonly string _connectionString;
        public XmlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveInSql(XmlDto dto)
        {
            string sql = @"INSERT INTO XmlDocuments 
                   (Type, [Key], XmlNumber, EmissionDate, IssuerDocument, 
                    SocialReasonIssuer, RecipientDocument, SocialReasonRecipient,
                    ServiceTakerCnpj, ShipperCnpj)
               VALUES 
                   (@Type, @Key, @XmlNumber, @EmissionDate, @IssuerDocument,
                    @SocialReasonIssuer, @RecipientDocument, @SocialReasonRecipient,
                    @ServiceTakerCnpj, @ShipperCnpj)";

            using var connection = new SqlConnection(_connectionString);
            connection.Execute(sql, dto);
        }
    }
}
