using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ERSetup_ExpenseLimitRepository : Repository<ERSetup_ExpenseLimit>
    {
        private AppDbContext _context;
        public ERSetup_ExpenseLimitRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ERSetup_ExpenseLimit Update(ERSetup_ExpenseLimit obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
