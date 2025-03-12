using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_LeaveTypeMasterRepository : Repository<HRA_LeaveTypeMaster>
    {
        private AppDbContext _context;
        public HRA_LeaveTypeMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_LeaveTypeMaster Update(HRA_LeaveTypeMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
