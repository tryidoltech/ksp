using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ACSubITDeductionModel
    {
        public AC_ITDeduction CM {  get; set; }
        public AC_ITSubdeduction CTY { get; set; }

        public string ITDeduction_Name {  get; set; }

        public ACSubITDeductionModel()
        {
            CM = new AC_ITDeduction();
            CTY = new AC_ITSubdeduction();
        }
    }
}
