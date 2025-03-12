using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAIncome_ITEmployeeParameterRepository : Repository<HRAIncome_ITEmployeeParameter>
    {
        private AppDbContext _context;
        public HRAIncome_ITEmployeeParameterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAIncome_ITEmployeeParameter Update(HRAIncome_ITEmployeeParameter obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
