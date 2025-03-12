using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_InvoiceTypeMasterRepository : Repository<AC_InvoiceTypeMaster>
    {
        private AppDbContext _context;
        public AC_InvoiceTypeMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_InvoiceTypeMaster Update(AC_InvoiceTypeMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
