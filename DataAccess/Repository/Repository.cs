using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual T Add(T obj)
        {
            //_context.Set<T>().Add(obj);
            _context.Entry(obj).State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();
            return obj;
        }

        public virtual int Delete(T obj)
        {
            _context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
            //_context.Set<T>().Remove(obj);
            return _context.SaveChanges();
        }

        public virtual List<T> Get()
        {
            var list = _context.Set<T>().AsNoTracking();
            return list.ToList();
        }

        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual T Update(T obj)
        {
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }
}
