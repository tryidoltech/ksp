using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_ExpenseSubUnitRepository : Repository<ER_ExpenseSubUnit>
    {
        private AppDbContext _context;
        public ER_ExpenseSubUnitRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_ExpenseSubUnit Update(ER_ExpenseSubUnit obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
