using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_ExpenseDataRangeRepository : Repository<ER_ExpenseDataRange>
    {
        private AppDbContext _context;
        public ER_ExpenseDataRangeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_ExpenseDataRange Update(ER_ExpenseDataRange obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
