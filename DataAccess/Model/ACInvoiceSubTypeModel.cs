using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ACInvoiceSubTypeModel
    {
        public AC_InvoiceTypeMaster CM { get; set; }
        public AC_InvoiceSubTypeMaster CTY { get; set; }

        public string Invoice_Type {  get; set; }

        public ACInvoiceSubTypeModel()
        {
            CM = new AC_InvoiceTypeMaster();
            CTY = new AC_InvoiceSubTypeMaster();
        }
    }
}
