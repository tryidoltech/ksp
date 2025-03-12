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
    public class AC_InvoiceTypeCodeMasterLogic : AC_InvoiceTypeCodeMasterRepository
    {
        private AppDbContext _context;
        public AC_InvoiceTypeCodeMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ACInvoiceTypeCodeModel> getInvoicetypecodeModel()
        {
            List<ACInvoiceTypeCodeModel> CSM = (from c in _context.AC_InvoiceTypeCodeMaster.Where(q => q.IsActive == true)
                                      join p in _context.AC_InvoiceTypeMaster.Where(s => s.IsActive == true)
                                          on c.InvoiceType_UUID equals p.UUID
                                      join f in _context.AC_InvoiceSubTypeMaster.Where(s => s.IsActive == true)
                                      on c.InvoiceSubType_UUID equals f.UUID
                                      select new ACInvoiceTypeCodeModel
                                      {
                                          CM = p,
                                          SM = f,
                                          CTY = c,
                                          //UUID = c.UUID,
                                          Invoice_Type = p.Invoice_Type,
                                          Title = f.Title,
                                      }).ToList();


            return CSM;
        }

    }
}
