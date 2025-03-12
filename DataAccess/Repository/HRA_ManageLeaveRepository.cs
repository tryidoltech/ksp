using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_ManageLeaveRepository : Repository<HRA_ManageLeave>
    {
        private AppDbContext _context;
        public HRA_ManageLeaveRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_ManageLeave Update(HRA_ManageLeave obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
