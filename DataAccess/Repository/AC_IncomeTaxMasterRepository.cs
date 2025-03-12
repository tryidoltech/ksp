using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_IncomeTaxMasterRepository : Repository<AC_IncomeTaxMaster>
    {
        private AppDbContext _context;
        public AC_IncomeTaxMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_IncomeTaxMaster Update(AC_IncomeTaxMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
