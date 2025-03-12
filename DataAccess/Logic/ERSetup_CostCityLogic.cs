using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ERSetup_CostCityLogic : ERSetup_CostCityRepository
    {
        private AppDbContext _context;
        public ERSetup_CostCityLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
