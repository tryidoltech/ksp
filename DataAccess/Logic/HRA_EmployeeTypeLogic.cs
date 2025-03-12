using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRA_EmployeeTypeLogic : HRA_EmployeeTypeRepository
    {
        private AppDbContext _context;
        public HRA_EmployeeTypeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
