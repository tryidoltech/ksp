using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_InvoiceSubTypeMasterLogic : AC_InvoiceSubTypeMasterRepository
    {
        private AppDbContext _context;
        public AC_InvoiceSubTypeMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ACInvoiceSubTypeModel> getInvoiceSubTypeModel()
        {
            List<ACInvoiceSubTypeModel> CSM = (from c in _context.AC_InvoiceSubTypeMaster.Where(q => q.IsActive == true)
                                               join p in _context.AC_InvoiceTypeMaster.Where(s => s.IsActive == true)
                                                   on c.Invoice_Type_UUID equals p.UUID

                                               select new ACInvoiceSubTypeModel
                                               {
                                                   CM = p,
                                                   CTY = c,
                                                   //UUID = c.UUID,
                                                   Invoice_Type = p.Invoice_Type,
                                               }).ToList();


            return CSM;
        }

    }
}
