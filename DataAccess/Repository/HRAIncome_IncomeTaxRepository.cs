using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAIncome_IncomeTaxRepository : Repository<HRAIncome_IncomeTax>
    {
        private AppDbContext _context;
        public HRAIncome_IncomeTaxRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAIncome_IncomeTax Update(HRAIncome_IncomeTax obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
