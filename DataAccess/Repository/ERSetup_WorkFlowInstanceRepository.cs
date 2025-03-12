using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ERSetup_WorkFlowInstanceRepository : Repository<ERSetup_WorkFlowInstance>
    {
        private AppDbContext _context;
        public ERSetup_WorkFlowInstanceRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ERSetup_WorkFlowInstance Update(ERSetup_WorkFlowInstance obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
