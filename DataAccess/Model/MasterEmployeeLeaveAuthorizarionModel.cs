using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class MasterEmployeeLeaveAuthorizarionModel
    {

        public Master_CompanyBranch CB { get; set; }
        public Master_Employee ME { get; set; }
        public HRA_LeaveTypeMaster LT { get; set; }
        public Master_Employee_leaveAuthorisation SM { get; set; }    
        public string UUID { get; set; }
        public string FirstName { get; set; }
        public string Branch_Name { get; set; }
        public string LastName { get; set; }

        public string LeaveType_Name { get; set; }
       

        public MasterEmployeeLeaveAuthorizarionModel()
        {

            CB = new Master_CompanyBranch();
            ME = new Master_Employee();
            LT = new HRA_LeaveTypeMaster();
            SM = new Master_Employee_leaveAuthorisation();
            
        }

    }
}
