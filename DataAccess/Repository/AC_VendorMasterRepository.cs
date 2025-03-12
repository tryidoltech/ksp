using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_VendorMasterRepository : Repository<AC_VendorMaster>
    {
        private AppDbContext _context;
        public AC_VendorMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_VendorMaster Update(AC_VendorMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
