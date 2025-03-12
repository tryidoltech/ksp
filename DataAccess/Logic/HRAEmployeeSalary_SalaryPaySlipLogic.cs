using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRAEmployeeSalary_SalaryPaySlipLogic : HRAEmployeeSalary_SalaryPaySlipRepository
    {
        private AppDbContext _context;
        public HRAEmployeeSalary_SalaryPaySlipLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
