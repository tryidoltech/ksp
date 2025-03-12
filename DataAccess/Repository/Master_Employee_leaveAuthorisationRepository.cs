using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_Employee_leaveAuthorisationRepository : Repository<Master_Employee_leaveAuthorisation>
    {
        private AppDbContext _context;
        public Master_Employee_leaveAuthorisationRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Employee_leaveAuthorisation Update(Master_Employee_leaveAuthorisation obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
