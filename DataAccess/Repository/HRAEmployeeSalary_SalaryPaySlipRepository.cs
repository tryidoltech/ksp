using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAEmployeeSalary_SalaryPaySlipRepository : Repository<HRAEmployeeSalary_SalaryPaySlip>
    {
        private AppDbContext _context;
        public HRAEmployeeSalary_SalaryPaySlipRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAEmployeeSalary_SalaryPaySlip Update(HRAEmployeeSalary_SalaryPaySlip obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
