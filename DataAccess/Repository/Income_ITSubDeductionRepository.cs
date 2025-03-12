using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Income_ITSubDeductionRepository : Repository<Income_ITSubDeduction>
    {
        private AppDbContext _context;
        public Income_ITSubDeductionRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Income_ITSubDeduction Update(Income_ITSubDeduction obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
