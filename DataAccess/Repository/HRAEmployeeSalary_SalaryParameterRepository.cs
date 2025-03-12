using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAEmployeeSalary_SalaryParameterRepository : Repository<HRAEmployeeSalary_SalaryParameter>
    {
        private AppDbContext _context;
        public HRAEmployeeSalary_SalaryParameterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAEmployeeSalary_SalaryParameter Update(HRAEmployeeSalary_SalaryParameter obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
