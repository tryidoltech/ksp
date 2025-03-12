using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_PaySlipCategoryRepository : Repository<HRA_PaySlipCategory>
    {
        private AppDbContext _context;
        public HRA_PaySlipCategoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_PaySlipCategory Update(HRA_PaySlipCategory obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
