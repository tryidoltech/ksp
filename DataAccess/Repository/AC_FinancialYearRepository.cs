using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_FinancialYearRepository : Repository<AC_FinancialYear>
    {
        private AppDbContext _context;
        public AC_FinancialYearRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_FinancialYear Update(AC_FinancialYear obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
