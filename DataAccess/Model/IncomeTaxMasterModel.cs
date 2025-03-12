using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class IncomeTaxMasterModel
    {
        public HRA_TaxRegime CM { get; set; }
        public Master_Gender SM { get; set; }
        public AC_IncomeTaxMaster CTY { get; set; }

        public string Gender_name { get; set; }
        public string Tax_Regime { get; set; }

        public IncomeTaxMasterModel()
        {
            CM = new HRA_TaxRegime();
            SM = new Master_Gender();
            CTY = new AC_IncomeTaxMaster();
        }
    }
}
