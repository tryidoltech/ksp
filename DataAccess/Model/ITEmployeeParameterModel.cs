using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ITEmployeeParameterModel
    {
        public AC_ITDeduction CM { get; set; }
        public Master_CompanyBranch SM { get; set; }
        public AC_FinancialYear FY { get; set; }
        public Master_Employee ME { get; set; }
        public AC_ITEmployeeParameter CTY { get; set; }

        public string Title { get; set; }
        public string Branch_Name { get; set; }
        public string ITDeduction_Name { get; set; }
        public string FullName { get; set; }


        public ITEmployeeParameterModel()
        {
            FY = new AC_FinancialYear();
            SM = new Master_CompanyBranch();
            ME = new Master_Employee();
            CM = new AC_ITDeduction();
            CTY= new AC_ITEmployeeParameter();
        }
    }
}
