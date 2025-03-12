using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_AllowanceTypeRepository : Repository<AC_AllowanceType>
    {
        private AppDbContext _context;
        public AC_AllowanceTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_AllowanceType Update(AC_AllowanceType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
