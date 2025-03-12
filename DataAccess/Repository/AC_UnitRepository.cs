using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_UnitRepository : Repository<AC_Unit>
    {
        private AppDbContext _context;
        public AC_UnitRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_Unit Update(AC_Unit obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
