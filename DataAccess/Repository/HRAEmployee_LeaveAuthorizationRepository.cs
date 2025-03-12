using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAEmployee_LeaveAuthorizationRepository : Repository<HRAEmployee_LeaveAuthorization>
    {
        private AppDbContext _context;
        public HRAEmployee_LeaveAuthorizationRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAEmployee_LeaveAuthorization Update(HRAEmployee_LeaveAuthorization obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
