using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ERSetup_CostCityRepository : Repository<ERSetup_CostCity>
    {
        private AppDbContext _context;
        public ERSetup_CostCityRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ERSetup_CostCity Update(ERSetup_CostCity obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
