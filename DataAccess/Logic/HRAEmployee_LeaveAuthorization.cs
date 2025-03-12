using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRAEmployee_LeaveAuthorizationLogic : HRAEmployee_LeaveAuthorizationRepository
    {
        private AppDbContext _context;
        public HRAEmployee_LeaveAuthorizationLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<HRALeaveAuthorizationModel> GetHraLeaveAuthorization()
        {


            var leaveauthorization = (from auth in _context.HRAEmployee_LeaveAuthorization.Where(e => e.IsActive == true)
                                 join branch in _context.Master_CompanyBranch.Where(cn => cn.IsActive == true)
                                 on auth.Company_UUID equals branch.UUID

                                 join emp in _context.Master_Employee.Where(c => c.IsActive == true)
                                 on auth.Employee_UUID equals emp.UUID

                                 join leave in _context.HRA_LeaveTypeMaster.Where(s => s.IsActive == true)
                                 on auth.LeaveType_UUID equals leave.UUID

                                 select new HRALeaveAuthorizationModel
                                 {
                                     SM = auth,
                                     ME = emp,
                                     LT = leave,
                                     CB = branch,                                    
                                     UUID = auth.UUID,
                                     FirstName = emp.FirstName + " " + emp.LastName,
                                     Branch_Name = branch.Branch_Name,
                                     LeaveType_Name = leave.LeaveType_Name
                                 }).ToList();

            return leaveauthorization;
        }
    }
}
