using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ITEmployeeParameterRepository : Repository<AC_ITEmployeeParameter>
    {
        private AppDbContext _context;
        public AC_ITEmployeeParameterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ITEmployeeParameter Update(AC_ITEmployeeParameter obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
