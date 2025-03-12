using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_InvoiceSubTypeMasterRepository : Repository<AC_InvoiceSubTypeMaster>
    {
        private AppDbContext _context;
        public AC_InvoiceSubTypeMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_InvoiceSubTypeMaster Update(AC_InvoiceSubTypeMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
