using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAIncome_ITDeductionRepository : Repository<HRAIncome_ITDeduction>
    {
        private AppDbContext _context;
        public HRAIncome_ITDeductionRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAIncome_ITDeduction Update(HRAIncome_ITDeduction obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
