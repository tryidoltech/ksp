using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_DepartmentLogic : Master_DepartmentRepository
    {
        private AppDbContext _context;
        public Master_DepartmentLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
