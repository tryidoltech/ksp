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
    public class ERSetup_WorkFlowInstanceLogic : ERSetup_WorkFlowInstanceRepository
    {
        private AppDbContext _context;
        public ERSetup_WorkFlowInstanceLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<ERSetupWorkFlowInstanceModel> GetERWorkFlow()
        {
            List<ERSetupWorkFlowInstanceModel> CSM = (from work in _context.ERSetup_WorkFlowInstance.Where(q => q.IsActive == true)
                                                      join ex in _context.ER_ExpenseType.Where(s => s.IsActive == true)
                                                          on work.ExpenseType_UUID equals ex.UUID
                                                      join des in _context.ER_ManageReportDesignation.Where(e => e.IsActive == true)
                                                          on work.ReportDesignation_UUID equals des.UUID
                                                      select new ERSetupWorkFlowInstanceModel
                                                      {
                                                          CB = ex,
                                                          LT = des,
                                                          SM = work,
                                                          ExpenseType_Name = ex.ExpenseType_Name,
                                                          Designation_Name = des.Designation_Name

                                                      }).ToList();
            return CSM;
        }
    }
}
