using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ERSetup_ExpenseEligibilityRepository : Repository<ERSetup_ExpenseEligibility>
    {
        private AppDbContext _context;
        public ERSetup_ExpenseEligibilityRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ERSetup_ExpenseEligibility Update(ERSetup_ExpenseEligibility obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
