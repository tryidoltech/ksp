using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ERSetup_ExpenseEligibilityLogic : ERSetup_ExpenseEligibilityRepository
    {
        private AppDbContext _context;
        public ERSetup_ExpenseEligibilityLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
