using Microsoft.EntityFrameworkCore;
using XmlReader.Data.Context;
using XmlReader.Data.Repositories;

namespace XmlReader.Data.Repositories
{
     public class Repository<T>(AppDbContext db) : IRepository<T> where T : class
     {
         private readonly AppDbContext _context = db;
         private readonly DbSet<T> _table = db.Set<T>();

         public void Create(T entity)
         {
             _table.Add(entity);
             _context.SaveChanges();
         }

         public IQueryable<T> GetAll()
         {
             return _table.AsQueryable();
         }

         public T GetById(string key)
         {
             var xml = _table.Find(key);

             return xml;
         }
     }
}
