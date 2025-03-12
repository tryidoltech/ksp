using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_EmployeeTypeRepository : Repository<HRA_EmployeeType>
    {
        private AppDbContext _context;
        public HRA_EmployeeTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_EmployeeType Update(HRA_EmployeeType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
