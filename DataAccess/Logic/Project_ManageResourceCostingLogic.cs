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
    public class Project_ManageResourceCostingLogic : Project_ManageResourceCostingRepository
    {
        private AppDbContext _context;
        public Project_ManageResourceCostingLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ManageResourceCostingModel> getManageResourceCostingSubModel()
        {
            List<ManageResourceCostingModel> CSM = (from c in _context.Project_ManageResourceCosting.Where(q => q.IsActive == true)
                                                    join s in _context.Project_CreateProject.Where(s => s.IsActive == true)
                                                        on c.Project_UUID equals s.UUID
                                                    join p in _context.Master_Employee.Where(e => e.IsActive == true)
                                                        on s.Employee_UUID equals p.UUID
                                                    select new ManageResourceCostingModel
                                                    {
                                                        PC = s,
                                                        ME = p,
                                                        PRC = c,
                                                        UUID = c.UUID,
                                                        Project_Title = s.Project_Title,
                                                        FullName = p.FirstName + "" + p.LastName,
                                                    }).ToList();
            return CSM;
        }

    }
}
