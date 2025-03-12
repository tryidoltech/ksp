using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_ExpenseUnitRepository : Repository<ER_ExpenseUnit>
    {
        private AppDbContext _context;
        public ER_ExpenseUnitRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_ExpenseUnit Update(ER_ExpenseUnit obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
