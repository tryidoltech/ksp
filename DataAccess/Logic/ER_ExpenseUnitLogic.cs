using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ER_ExpenseUnitLogic : ER_ExpenseUnitRepository
    {
        private AppDbContext _context;
        public ER_ExpenseUnitLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
