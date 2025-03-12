using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_EmployeeRepository : Repository<Master_Employee>
    {
        private AppDbContext _context;
        public Master_EmployeeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Employee Update(Master_Employee obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
