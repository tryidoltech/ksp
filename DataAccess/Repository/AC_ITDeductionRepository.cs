using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ITDeductionRepository : Repository<AC_ITDeduction>
    {
        private AppDbContext _context;
        public AC_ITDeductionRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ITDeduction Update(AC_ITDeduction obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
