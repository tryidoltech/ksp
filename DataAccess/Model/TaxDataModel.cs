using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class TaxDataModel
    {
        public AC_TaxGroup CM { get; set; }
        public AC_Taxcode SM { get; set; }
        public AC_TaxData CTY { get; set; }

        public string TaxGroup_Name { get; set; }
        public string Tax_Name { get; set; }

        public TaxDataModel()
        {
            CM = new AC_TaxGroup();
            SM = new AC_Taxcode();
            CTY = new AC_TaxData();
        }
    }
}
