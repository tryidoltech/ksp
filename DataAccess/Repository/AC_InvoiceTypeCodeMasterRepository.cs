using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_InvoiceTypeCodeMasterRepository : Repository<AC_InvoiceTypeCodeMaster>
    {
        private AppDbContext _context;
        public AC_InvoiceTypeCodeMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_InvoiceTypeCodeMaster Update(AC_InvoiceTypeCodeMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
