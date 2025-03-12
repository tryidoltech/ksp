using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class HRALeaveAuthorizationModel
    {

        public Master_CompanyBranch CB { get; set; }
        public Master_Employee ME { get; set; }
        public HRA_LeaveTypeMaster LT { get; set; }
        public HRAEmployee_LeaveAuthorization SM { get; set; }    
        public string UUID { get; set; }
        public string FirstName { get; set; }
        public string Branch_Name { get; set; }
        public string LastName { get; set; }

        public string LeaveType_Name { get; set; }
       

        public HRALeaveAuthorizationModel()
        {

            CB = new Master_CompanyBranch();
            ME = new Master_Employee();
            LT = new HRA_LeaveTypeMaster();
            SM = new HRAEmployee_LeaveAuthorization();
            
        }

    }
}
