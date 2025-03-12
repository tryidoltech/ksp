using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class TaxCodeModel
    {
        public AC_TaxGroup CM { get; set; }
        public AC_Taxcode CTY { get; set; }

        public string TaxGroup_Name { get; set; }

        public TaxCodeModel()
        {
            CM = new AC_TaxGroup();
            CTY = new AC_Taxcode();
        }
    }
}
