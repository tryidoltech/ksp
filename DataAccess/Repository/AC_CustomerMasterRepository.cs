using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_CustomerMasterRepository : Repository<AC_CustomerMaster>
    {
        private AppDbContext _context;
        public AC_CustomerMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_CustomerMaster Update(AC_CustomerMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
