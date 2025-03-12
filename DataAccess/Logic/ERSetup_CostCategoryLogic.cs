using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ERSetup_CostCategoryLogic : ERSetup_CostCategoryRepository
    {
        private AppDbContext _context;
        public ERSetup_CostCategoryLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
