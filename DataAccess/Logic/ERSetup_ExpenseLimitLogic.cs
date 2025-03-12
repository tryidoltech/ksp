using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ERSetup_ExpenseLimitLogic : ERSetup_ExpenseLimitRepository
    {
        private AppDbContext _context;
        public ERSetup_ExpenseLimitLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
