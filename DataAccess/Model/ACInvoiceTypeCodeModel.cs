using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ACInvoiceTypeCodeModel
    {
        public AC_InvoiceTypeMaster CM { get; set; }
        public AC_InvoiceSubTypeMaster SM { get; set; }
        public AC_InvoiceTypeCodeMaster CTY { get; set; }

        public string Title { get; set; }
        public string Invoice_Type { get; set; }

        public ACInvoiceTypeCodeModel()
        {
            CM = new AC_InvoiceTypeMaster();
            SM = new AC_InvoiceSubTypeMaster();
            CTY = new AC_InvoiceTypeCodeMaster();
        }
    }
}
