using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Select();
        T Select(int id);
        T Insert(T entity);
        T Update(T enitity);
        void Delete(int id);
    }
}
