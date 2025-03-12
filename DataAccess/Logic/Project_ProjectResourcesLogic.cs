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
    public class Project_ProjectResourcesLogic : Project_ProjectResourcesRepository
    {
        private AppDbContext _context;
        public Project_ProjectResourcesLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ManagePageResourseModel> getManageProjectResourseSubModel()
        {
            List<ManagePageResourseModel> CSM = (from c in _context.Project_ProjectResources.Where(q => q.IsActive == true)
                                                 join s in _context.Project_CreateProject.Where(s => s.IsActive == true)
                                                     on c.Project_UUID equals s.UUID
                                                 join p in _context.Master_Designation.Where(e => e.IsActive == true)
                                                     on c.Designation_UUID equals p.UUID
                                                 select new ManagePageResourseModel
                                                 {
                                                     CM = s,
                                                     SM = p,
                                                     CTY = c,

                                                     Project_Name = s.Project_Title,
                                                     Designation_Name = p.Designation_Name,
                                                 }).ToList();
            return CSM;
        }

    }
}
