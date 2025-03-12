using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRAIncome_ITDeductionLogic : HRAIncome_ITDeductionRepository
    {
        private AppDbContext _context;
        public HRAIncome_ITDeductionLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
