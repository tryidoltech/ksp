using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_NomenClatureRepository : Repository<AC_NomenClature>
    {
        private AppDbContext _context;
        public AC_NomenClatureRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_NomenClature Update(AC_NomenClature obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
