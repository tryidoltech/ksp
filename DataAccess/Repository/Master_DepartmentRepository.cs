using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_DepartmentRepository : Repository<Master_Department>
    {
        private AppDbContext _context;
        public Master_DepartmentRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Department Update(Master_Department obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
