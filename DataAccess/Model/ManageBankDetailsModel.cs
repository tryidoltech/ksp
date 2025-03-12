using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ManageBankDetailsModel
    {
        public Master_Employee CM { get; set; }
        public Master_Banktype SM { get; set; }
        public Master_BankACtype BM { get; set; }
        public Master_ManageBankDetail CTY { get; set; }

        public string FullName { get; set; }
        public string Bank_name { get; set; }
        public string Bank_AccontType { get; set; }

        public ManageBankDetailsModel()
        {
            CM = new Master_Employee();
            SM = new Master_Banktype();
            BM = new Master_BankACtype();
            CTY = new Master_ManageBankDetail();
        }

    }
}
