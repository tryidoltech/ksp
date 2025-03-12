using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    interface IRepository<T> where T : class
    {
        List<T> Get();

        T Get(int id);

        T Add(T obj);

        T Update(T obj);

        int Delete(T obj);
    }
}
