using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ERSetup_CostCategoryRepository : Repository<ERSetup_CostCategory>
    {
        private AppDbContext _context;
        public ERSetup_CostCategoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ERSetup_CostCategory Update(ERSetup_CostCategory obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
