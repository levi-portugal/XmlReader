using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using XmlReader.Data.Context;
using XmlReader.Dtos;
using XmlReader.Entities;

namespace XmlReader.Data.Repository
{
    public class XmlRepository
    {
        private readonly AppDbContext _context;

        public XmlRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Save(XML entity)
        {
            // O EF Core gera o SQL de INSERT automaticamente, vai dar bom 
            _context.XmlDocuments.Add(entity);
            _context.SaveChanges();
        }

        public void SaveRange(List<XML> entities)
        {
            _context.XmlDocuments.AddRange(entities);
            _context.SaveChanges();
        }
    }
}
