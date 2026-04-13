using System;
using System.Collections.Generic;
using System.Text;

namespace XmlReader.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(string id);
        void Create(T entity);
    }
}
